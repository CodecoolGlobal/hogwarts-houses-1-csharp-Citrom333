using System.Collections.Generic;
using HogwartsHouses.Models;

namespace HogwartsHouses.DAL
{
    public interface IRepository<T>
    {
        public HashSet<T> All();
        public void Add(T toAdd);
        public void Remove(T toRemove);
        public void Update(T toUpdate);
        public bool AddRoomToStudent(Room room, Student student);
    }
}
