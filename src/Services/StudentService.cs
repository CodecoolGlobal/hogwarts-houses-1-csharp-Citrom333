using System;
using System.Collections.Generic;
using System.Linq;
using HogwartsHouses.DAL;
using HogwartsHouses.Models;
using HogwartsHouses.Models.Types;

namespace HogwartsHouses.Services;

public class StudentService:IStudentService
{
    private IRepository<Student> StudentRepository { get; }

    public StudentService(IRepository<Student> studentRepository)
    {
        StudentRepository = studentRepository;
    }

    public List<Student> GetAllStudents()
    {
        return StudentRepository.All().ToList();
    }

    public void AddStudent(Student student)
    {
        StudentRepository.Add(student);
    }

    public void DeleteStudent(int id)
    {
        StudentRepository.Remove(new Student(id, default, default,default,default));
    }

    public void UpdateStudent(Student student)
    {
        StudentRepository.Update(student);
    }
}