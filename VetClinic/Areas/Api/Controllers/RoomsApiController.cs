using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetClinic.Models;
using System.Collections.Generic;
using System.Linq;

namespace VetClinic.Areas.Api.Controllers
{
    /// <summary>
    /// API controller for managing rooms in the veterinary clinic.
    /// Provides endpoints for CRUD operations.
    /// </summary>
    [Route("Api/Rooms")]
    [ApiController]
    public class RoomsApiController : ControllerBase
    {
        private readonly VetClinicContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomsApiController"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public RoomsApiController(VetClinicContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all rooms, including their appointment details.
        /// </summary>
        /// <returns>A list of rooms with appointment details.</returns>
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

        /// <summary>
        /// Retrieves a specific room by its ID.
        /// </summary>
        /// <param name="id">The ID of the room.</param>
        /// <returns>The requested room if found; otherwise, NotFound.</returns>
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

        /// <summary>
        /// Creates a new room.
        /// </summary>
        /// <param name="room">The room details.</param>
        /// <returns>The created room with its assigned ID.</returns>
        [HttpPost]
        public ActionResult<Room> CreateRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Rooms.Add(room);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRoomById), new { id = room.RoomId }, room);
        }

        /// <summary>
        /// Updates an existing room.
        /// </summary>
        /// <param name="id">The ID of the room to update.</param>
        /// <param name="room">The updated room details.</param>
        /// <returns>The updated room if successful; otherwise, BadRequest or NotFound.</returns>
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

        /// <summary>
        /// Deletes a room if it has no associated appointments.
        /// </summary>
        /// <param name="id">The ID of the room to delete.</param>
        /// <returns>NoContent if successful; otherwise, NotFound or Conflict.</returns>
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
