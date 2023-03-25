using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.StaticFiles;

namespace HogwartsHouses.Models
{
    [System.Serializable]
    public class Room
    {
        public int Id { get; }
        public int NumberOfBeds { get; set; }
        public List<Student> OccupiedBeds { get; set; }
        public bool RoomForBoys { get; set; }

        public Room(int id, int numberOfBeds, bool boyRoom)
        {
            Id = id;
            NumberOfBeds = numberOfBeds;
            RoomForBoys = boyRoom;
            OccupiedBeds = new List<Student>(NumberOfBeds);
           
        }
    }
}
