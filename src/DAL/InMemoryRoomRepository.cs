using System;
using System.Collections.Generic;
using System.Linq;
using HogwartsHouses.Models;

namespace HogwartsHouses.DAL
{
    public class InMemoryRoomRepository : IRepository<Room>
    {
        private HashSet<Room> _rooms;
        private IRepository<Room> _repositoryImplementation;
        public InMemoryRoomRepository()
        {
            SeedRooms();
        }

        private void SeedRooms()
        {
            _rooms = new HashSet<Room>();
        }

        public HashSet<Room> All()
        {
            return _rooms;
        }

        public void Add(Room toAdd)
        {
            var id = _rooms.Count < 1 ? 1 : _rooms.ToList().MaxBy(r => r.Id)!.Id + 1;
            _rooms.Add(new Room(id, toAdd.NumberOfBeds, toAdd.RoomForBoys));
        }

        public void Remove(Room toRemove)
        {
            _rooms.RemoveWhere(room => room.Id == toRemove.Id);
        }

        public void Update(Room toUpdate)
        {
            foreach (var room in _rooms)
            {
                if (room.Id == toUpdate.Id)
                {
                    room.NumberOfBeds = toUpdate.NumberOfBeds;
                    room.RoomForBoys = toUpdate.RoomForBoys;
                    room.OccupiedBeds.Clear();
                }
            }
        }

        public bool AddRoomToStudent(Room room, Student student)
        {
            if (room.OccupiedBeds.Capacity < 0)
                return false;
            foreach (var r in _rooms)
            {
                if (r.Id == room.Id)
                {
                   r.OccupiedBeds.Add(student);
            return true;
                }
            }
            return false;
        }
    }
}
