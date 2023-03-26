using System;
using System.Collections.Generic;
using HogwartsHouses.Models;

namespace HogwartsHouses.Services
{
    public interface IRoomService
    {
        public List<Room> GetAllRooms();
        public void AddRoom(Tuple<int, bool> roomDetails);
        public void DeleteRoom(int id);
        public void UpdateRoom(int id, Tuple<int, bool> roomDetails);
        public bool PutStudentToRoom(int roomId, int studentId);
        public List<Room> GetRoomsWithFreeBed();
        public List<Room> GetRoomsForRatKeepers();
    }
}