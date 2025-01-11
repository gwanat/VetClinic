using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetClinic.Models;
using System.Collections.Generic;
using System.Linq;

namespace VetClinic.Areas.Api.Controllers
{
    [Route("Api/Rooms")]
    [ApiController]
    public class RoomsApiController : ControllerBase
    {
        private readonly VetClinicContext _context;

        public RoomsApiController(VetClinicContext context)
        {
            _context = context;
        }

        // GET: api/rooms
        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetRooms()
        {
            var rooms = _context.Rooms
                .Include(r => r.Appointments)
                .ToList();

            foreach (var room in rooms)
            {
                room.UpdateOccupationStatus();
            }

            return Ok(rooms);
        }

        // GET: api/rooms/{id}
        [HttpGet("{id}")]
        public ActionResult<Room> GetRoomById(int id)
        {
            var room = _context.Rooms
                .Include(r => r.Appointments)
                .FirstOrDefault(r => r.RoomId == id);

            if (room == null)
                return NotFound();

            room.UpdateOccupationStatus();

            return Ok(room);
        }

        // POST: api/rooms
        [HttpPost]
        public ActionResult<Room> CreateRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Rooms.Add(room);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRoomById), new { id = room.RoomId }, room);
        }

        // PUT: api/rooms/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] Room room)
        {
            if (id != room.RoomId || !ModelState.IsValid)
                return BadRequest();

            var existingRoom = _context.Rooms.FirstOrDefault(r => r.RoomId == id);
            if (existingRoom == null)
                return NotFound();

            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.Type = room.Type;
            existingRoom.IsOccupied = room.IsOccupied;
            existingRoom.Description = room.Description;

            _context.SaveChanges();
            return Ok(existingRoom);
        }

        // DELETE: api/rooms/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = _context.Rooms
                .Include(r => r.Appointments)
                .FirstOrDefault(r => r.RoomId == id);

            if (room == null)
                return NotFound();

            if (room.Appointments != null && room.Appointments.Any())
            {
                return Conflict(new { message = "Cannot delete the room because it has associated appointments." });
            }

            _context.Rooms.Remove(room);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
