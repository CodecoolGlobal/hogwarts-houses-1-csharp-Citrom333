using System;
using System.Collections.Generic;
using System.Linq;
using HogwartsHouses.Models;
using HogwartsHouses.Services;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsHouses.Controllers;

[ApiController, Route("/rooms")]
public class RoomController : Controller
{
    private IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet("AllRooms")]
    public List<Room> GetAllRooms()
    {
        return _roomService.GetAllRooms();
    }

    [HttpGet("Room/{id}")]
    public Room GetRooms(int id)
    {
        return _roomService.GetAllRooms().FirstOrDefault(room => room.Id == id);
    }

    [HttpPost("NewRoom")]
    public void AddNewRoom([FromBody] Tuple<int, bool> roomDetails)
    {
        Console.WriteLine($"New room with {roomDetails.Item1} beds, for {(roomDetails.Item2 ? "boys" : "girls")}");

        _roomService.AddRoom(roomDetails);
    }

    [HttpDelete("DeleteRoom/{id}")]
    public void DeleteRoom(int id)
    {
        _roomService.DeleteRoom(id);
    }

    [HttpDelete("RenovateRoom/{id}")]
    public void UpdateRoom(int id, [FromBody] Tuple<int, bool> roomDetails)
    {
        _roomService.UpdateRoom(id, roomDetails);
    }

    [HttpPut("PutToRoom/{id}/{studentId}")]
    public void PutStudentToRoom(int id, int studentId)
    {
        _roomService.PutStudentToRoom(id, studentId);
        Console.WriteLine($"{studentId} student is in {id} room");
    }

    [HttpGet("available")]
    public List<Room> GetRoomsWithFreeSpace()
    {
        return _roomService.GetRoomsWithFreeBed();
    }

    [HttpGet("rat-owners")]
    public List<Room> GetRoomsForRats()
    {
        return _roomService.GetRoomsForRatKeepers();
    }
}