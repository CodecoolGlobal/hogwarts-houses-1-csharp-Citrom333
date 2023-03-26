using System;
using System.Collections.Generic;
using System.Linq;
using HogwartsHouses.Models;
using HogwartsHouses.Models.Types;
using HogwartsHouses.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HogwartsHouses.Controllers;

// [ApiController, 
[Route("/students")]
public class StudentController : Controller
{
    private IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    private String MakeTheListToString(List<Student> students)
    {
        string stString = "";
        foreach (var student in students)
        {
            stString += $"</br>{student.ToString()}";
        }

        return stString;
    }

    [HttpGet("AllStudents")]
    public IActionResult GetAll()
    {
        ViewData["All"] = MakeTheListToString(_studentService.GetAllStudents());
        return View();
    }

    [HttpGet("Student")]
    public IActionResult GetStudents()
    {
        return View("FindStudent");
    }

    [HttpPost("Student")]
    public IActionResult GetStudents(Student student)
    {
        int id = student.StudentId;
        if (_studentService.GetAllStudents().Select(s => s.StudentId).Contains(id))
        {
            Console.WriteLine(id);
            ViewData["result"] = _studentService.GetAllStudents().FirstOrDefault(student => student.StudentId == id)
                .ToString();
            return View("Success");
        }
        else
        {
            return View("WrongFillOut");
        }
    }

    [HttpGet("NewStudent")]
    public IActionResult AddNewStudent()
    {
        return View();
    }

    [HttpPost("NewStudent/{overload}")]
    public IActionResult AddNewStudent([FromBody] Student student, bool overload)
    {
        _studentService.AddStudent(student);
        ViewData["result"] = $"New Student {student.Name}, in {student.House}";
        return View("Success");
    }

    [HttpPost("NewStudent")]
    public IActionResult AddNewStudent(Student student)
    {
        string name = student.Name;
        Gender gender = student.Gender;
        PetType pet = student.Pet;
        HouseType house = student.House;
        if (name == null || gender == 0 || pet == 0 || house == 0)
        {
            return View("WrongFillOut");
        }
        else
        {
            _studentService.AddStudent(new Student(default, name, gender, pet, house));
            ViewData["house"] = house;
            ViewData["name"] = name;
            return View("SortingHat");
        }
    }

    [HttpDelete("DeleteStudent/{id}")]
    public IActionResult DeleteStudent(int id)
    {
        _studentService.DeleteStudent(id);
        return Content($"New Student {id} deleted.");
    }

    [HttpPut("UpdateStudent/{id}")]
    public IActionResult UpdateStudent(int id, [FromBody] Student studentDetails)
    {
        _studentService.UpdateStudent(new Student(id, studentDetails.Name, studentDetails.Gender, studentDetails.Pet,
            studentDetails.House));
        ViewData["result"] = $"New Student {studentDetails.Name} updated.";
        return View("Success");
    }
}