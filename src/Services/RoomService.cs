using System;
using System.Collections.Generic;
using System.Linq;
using HogwartsHouses.DAL;
using HogwartsHouses.Models;
using HogwartsHouses.Models.Types;

namespace HogwartsHouses.Services
{
    public class RoomService : IRoomService
    {
        private IRepository<Room> _repository { get; }
        private IRepository<Student> StudentRepository { get; }
        public RoomService(IRepository<Room> repository, IRepository<Student> studentRepository)
        {
            _repository = repository;
            StudentRepository = studentRepository;
        }

        public List<Room> GetAllRooms()
        {
            return _repository.All().ToList();
        }

        public void AddRoom(Tuple<int, bool> roomDetails)
        {
            _repository.Add(new Room(default,roomDetails.Item1, roomDetails.Item2));
        }

        public void DeleteRoom(int id)
        {
            _repository.Remove(new Room(id,default,default));
        }

        public void UpdateRoom(int id, Tuple<int, bool> roomDetails)
        {
            _repository.Update(new Room(id, roomDetails.Item1, roomDetails.Item2));
        }

        public void PutStudentToRoom(int roomId, int studentId)
        {
            var room = _repository.All().ToList().FirstOrDefault(r => r.Id == roomId);
            var student= StudentRepository.All().ToList().FirstOrDefault(s => s.StudentId == studentId);
            if (StudentRepository.AddRoomToStudent(room, student))
                _repository.AddRoomToStudent(room, student);
        }

        public List<Room> GetRoomsWithFreeBed()
        {
            return _repository.All().Where(r => r.OccupiedBeds.Count < r.NumberOfBeds).ToList();
        }

        public List<Room> GetRoomsForRatKeepers()
        {
            return _repository.All().ToList().FindAll(room =>
                !room.OccupiedBeds.Select(student => student.Pet).Contains(PetType.Cat) &&
                 !room.OccupiedBeds.Select(student => student.Pet).Contains(PetType.Owl));
        }
    }
}
