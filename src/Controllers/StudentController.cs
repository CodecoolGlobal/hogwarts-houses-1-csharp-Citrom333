using System;
using System.Collections.Generic;
using System.Linq;
using HogwartsHouses.Models;
using HogwartsHouses.Services;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsHouses.Controllers;
[ApiController, Route("/students")]
public class StudentController : Controller
{
    private IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    [HttpGet("AllStudents")]
    public List<Student> GetAll()
    {
        return _studentService.GetAllStudents();
    }
    [HttpGet("Student/{id}")]
    public Student GetStudents(int id)
    {
        return _studentService.GetAllStudents().FirstOrDefault(student => student.StudentId==id);
    }
    
    [HttpPost("NewStudent")]
    public void AddNewStudent([FromBody] Student student)
    {
        Console.WriteLine($"New Student {student.Name}, in {student.House}");
        _studentService.AddStudent(student);
    }
    [HttpDelete("DeleteStudent/{id}")]
    public void DeleteStudent(int id)
    {
        _studentService.DeleteStudent(id);
    }
    [HttpDelete("UpdateStudent/{id}")]
    public void UpdateStudent(int id, [FromBody] Student studentDetails)
    {
        _studentService.UpdateStudent(new Student(id, studentDetails.Name, studentDetails.Gender,studentDetails.Pet, studentDetails.House));
    }
}