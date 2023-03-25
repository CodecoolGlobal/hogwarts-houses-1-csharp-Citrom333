using System.Collections.Generic;
using System.Linq;
using HogwartsHouses.Models;
using HogwartsHouses.Models.Types;

namespace HogwartsHouses.DAL;

public class StudentRepository : IRepository<Student>
{
    private HashSet<Student> _students;

    public StudentRepository()
    {
        _students = new HashSet<Student>();
    }

    public HashSet<Student> All()
    {
        return _students;
    }

    public void Add(Student toAdd)
    {
         var id = _students.Count < 1 ? 1 : _students.ToList().MaxBy(s => s.StudentId)!.StudentId + 1;
        _students.Add(new Student(id, toAdd.Name, toAdd.Gender, toAdd.Pet, toAdd.House));
    }

    public void Remove(Student toRemove)
    {
        _students.RemoveWhere(student => toRemove.StudentId == student.StudentId);
    }

    public void Update(Student toUpdate)
    {
        foreach (var student in _students)
        {
            if (student.StudentId==toUpdate.StudentId)
            {
                student.Pet = toUpdate.Pet;
            }
        }
    }

    public bool AddRoomToStudent(Room room, Student student)
    {
        return ((student.Gender == Gender.Boy && room.RoomForBoys) ||
                (student.Gender == Gender.Girl && !room.RoomForBoys)) && room.OccupiedBeds.Count < room.NumberOfBeds;
    }
}