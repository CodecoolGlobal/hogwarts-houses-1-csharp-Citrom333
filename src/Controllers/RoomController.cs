using System;
using System.Collections.Generic;
using System.Linq;
using HogwartsHouses.Models;
using HogwartsHouses.Models.Types;
using HogwartsHouses.Services;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsHouses.Controllers;

// [ApiController, 
[Route("/rooms")]
public class RoomController : Controller
{
    private IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    private String MakeTheListToString(List<Room> rooms)
    {
        string roomString = "";
        foreach (var room in rooms)
        {
            roomString += $"</br>{room.ToString()}";
        }

        return roomString;
    }

    [HttpGet("AllRooms")]
    public IActionResult GetAllRooms()
    {
        ViewData["All"] = MakeTheListToString(_roomService.GetAllRooms());
        ViewData["Filter"] = "List of all rooms.";
        return View("GetRooms");
    }

    [HttpGet("Room")]
    public IActionResult GetRoom()
    {
        return View("SearchRoom");
    }
    [HttpPost("Room")]
    public IActionResult GetRoom(Room room)
    {
        int id = room.Id;
        if (_roomService.GetAllRooms().Select(r => r.Id).Contains(id))
        {
            ViewData["result"] = _roomService.GetAllRooms().FirstOrDefault(room => room.Id == id).ToString();
            return View("Success");
        }
        else
        {
            return View("WrongFillOut");
        }
    }

    [HttpPost("NewRoomFromPostman")]
    public void AddNewRoom([FromBody] Tuple<int, bool> roomDetails)
    {
        Console.WriteLine($"New room with {roomDetails.Item1} beds, for {(roomDetails.Item2 ? "boys" : "girls")}");

        _roomService.AddRoom(roomDetails);
    }

    [HttpGet("NewRoom")]
    public IActionResult AddNewRoom()
    {
        return View();
    }

    [HttpPost("NewRoom")]
    public IActionResult AddNewRoom(Room room)
    {
        int numberOfBeds = room.NumberOfBeds;
        bool roomForBoys = room.RoomForBoys;

        if (numberOfBeds == 0 || roomForBoys == null)
        {
            return View("WrongFillOut");
        }
        else
        {
            ViewData["result"] = $"New room with {room.NumberOfBeds} beds, for {(room.RoomForBoys ? "boys" : "girls")}";
            _roomService.AddRoom(new Tuple<int, bool>(room.NumberOfBeds, room.RoomForBoys));
            return View("Success");
        }
    }

    [HttpGet("DeleteRoom")]
    public IActionResult DeleteRoom()
    {
        return View();
    }

    [HttpPost("DeleteRoom")]
    public IActionResult DeleteRoom(Room room)
    {
        int id = room.Id;
        Console.WriteLine(id);
        if (_roomService.GetAllRooms().Select(r => r.Id).Contains(id))
        {
            ViewData["result"] = $"Room nr.{id} was deleted successfully";
            _roomService.DeleteRoom(id);
            return View("Success");
        }
        else
        {
            Console.Write("Delete went wrong");
            return View("WrongFillOut");
        }
    }

    [HttpGet("RenovateRoom")]
    public IActionResult UpdateRoom()
    {
        return View("RenovateRoom");
    }

    [HttpPost("RenovateRoom")]
    public IActionResult UpdateRoom(Room room)
    {
        int id = room.Id;
        int numberOfBeds = room.NumberOfBeds;
        bool roomForBoys = room.RoomForBoys;
        if (_roomService.GetAllRooms().Select(r => r.Id).Contains(id) && numberOfBeds != 0)
        {
            _roomService.UpdateRoom(id, new Tuple<int, bool>(numberOfBeds, roomForBoys));
            ViewData["result"] = $"Room nr.{room.Id} was renovated successfully.";
            return View("Success");
        }
        else
        {
            Console.Write("Update went wrong");
            return View("WrongFillOut");
        }
    }

    [HttpPut("PutToRoomFromPostman/{id}/{studentId}")]
    public void PutStudentToRoom(int id, int studentId)
    {
        _roomService.PutStudentToRoom(id, studentId);
        Console.WriteLine($"{studentId} student is in {id} room");
    }

    [HttpGet("PutToRoom")]
    public IActionResult PutStudentToRoom()
    {
        return View("MoveStudentToRoom");
    }
    Tuple<Room, Student> _roomAndStudent = new(new Room(1, 1, true), new Student(1, " ", Gender.Boy, PetType.Cat, HouseType.Gryffindor));
    [HttpPost("PutToRoom")]
    public IActionResult PutStudentToRoom(ViewModelForBoth both)
    {
        int roomId = both.Room.Id;
        int studentId = both.Student.StudentId;
        if (_roomService.GetAllRooms().Select(r => r.Id).Contains(roomId) &&
            _roomService.PutStudentToRoom(roomId, studentId))
        {
            ViewData["result"] = $"Student (Id:{studentId})  is in room Nr.{roomId}. ";
            return View("Success");
        }
        else

        {
            Console.WriteLine($"room {roomId}, student {studentId}");
            return View("WrongFillOut");
        }
    }

    [HttpGet("available")]
    public IActionResult GetRoomsWithFreeSpace()
    {
        ViewData["All"] = MakeTheListToString(_roomService.GetRoomsWithFreeBed());
        ViewData["Filter"] = "List of rooms with unoccupied bed.";
        return View("GetRooms");
    }

    [HttpGet("rat-owners")]
    public IActionResult GetRoomsForRats()
    {
        ViewData["All"] = MakeTheListToString(_roomService.GetRoomsForRatKeepers());
        ViewData["Filter"] = "List of rooms that are good for rat keepers";
        return View("GetRooms");
    }
}