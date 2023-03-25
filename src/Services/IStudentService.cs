using System;
using System.Collections.Generic;
using HogwartsHouses.Models;

namespace HogwartsHouses.Services;

public interface IStudentService
{
    public List<Student> GetAllStudents();
    public void AddStudent(Student student);
    public void DeleteStudent(int id);
    public void UpdateStudent(Student student);
}