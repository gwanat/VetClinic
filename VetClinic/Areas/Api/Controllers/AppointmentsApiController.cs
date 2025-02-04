using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetClinic.Models;
using System.Collections.Generic;
using System.Linq;

namespace VetClinic.Areas.Api.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsApiController : ControllerBase
    {
        private readonly VetClinicContext _context;

        public AppointmentsApiController(VetClinicContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all appointments.
        /// </summary>
        /// <returns>A list of appointments.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<object>> GetAppointments()
        {
            var appointments = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.Room)
                .Select(a => new
                {
                    a.AppointmentId,
                    a.StartTime,
                    a.EndTime,
                    DoctorName = a.Doctor != null ? a.Doctor.Name : "N/A",
                    PatientName = a.Patient != null ? a.PatientName : "N/A",
                    ClientName = !string.IsNullOrEmpty(a.ClientName) ? a.ClientName : "N/A",
                    RoomNumber = a.Room != null ? a.Room.RoomNumber.ToString() : "N/A"
                })
                .ToList();

            return Ok(appointments);
        }

        /// <summary>
        /// Retrieves a specific appointment by ID.
        /// </summary>
        /// <param name="id">The ID of the appointment.</param>
        /// <returns>The appointment details.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<object> GetAppointmentById(int id)
        {
            var appointment = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.Room)
                .FirstOrDefault(a => a.AppointmentId == id);

            if (appointment == null)
                return NotFound(new { Message = "Appointment not found" });

            return Ok(new
            {
                appointment.AppointmentId,
                appointment.StartTime,
                appointment.EndTime,
                DoctorName = appointment.Doctor?.Name ?? "N/A",
                PatientName = appointment.PatientName ?? "N/A",
                ClientName = appointment.ClientName ?? "N/A",
                RoomNumber = appointment.Room?.RoomNumber ?? "N/A"
            });
        }

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="appointment">Appointment details.</param>
        /// <returns>Confirmation of creation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult CreateAppointment([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hasConflict = _context.Appointments.Any(a =>
                (a.RoomId == appointment.RoomId &&
                 ((appointment.StartTime >= a.StartTime && appointment.StartTime < a.EndTime) ||
                  (appointment.EndTime > a.StartTime && appointment.EndTime <= a.EndTime))) ||
                (a.DoctorId == appointment.DoctorId &&
                 ((appointment.StartTime >= a.StartTime && appointment.StartTime < a.EndTime) ||
                  (appointment.EndTime > a.StartTime && appointment.EndTime <= a.EndTime)))
            );

            if (hasConflict)
                return Conflict(new { Message = "The appointment overlaps with an existing appointment." });

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.AppointmentId }, appointment);
        }

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="id">Appointment ID.</param>
        /// <param name="appointment">Updated appointment details.</param>
        /// <returns>Confirmation of update.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult UpdateAppointment(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.AppointmentId || !ModelState.IsValid)
                return BadRequest(new { Message = "Invalid request" });

            var existingAppointment = _context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
            if (existingAppointment == null)
                return NotFound(new { Message = "Appointment not found" });

            var hasConflict = _context.Appointments.Any(a =>
                a.AppointmentId != id &&
                ((a.RoomId == appointment.RoomId &&
                 ((appointment.StartTime >= a.StartTime && appointment.StartTime < a.EndTime) ||
                  (appointment.EndTime > a.StartTime && appointment.EndTime <= a.EndTime))) ||
                (a.DoctorId == appointment.DoctorId &&
                 ((appointment.StartTime >= a.StartTime && appointment.StartTime < a.EndTime) ||
                  (appointment.EndTime > a.StartTime && appointment.EndTime <= a.EndTime)))));

            if (hasConflict)
                return Conflict(new { Message = "The appointment overlaps with an existing appointment." });

            existingAppointment.StartTime = appointment.StartTime;
            existingAppointment.EndTime = appointment.EndTime;
            existingAppointment.DoctorId = appointment.DoctorId;
            existingAppointment.PatientId = appointment.PatientId;
            existingAppointment.RoomId = appointment.RoomId;

            _context.SaveChanges();

            return Ok(new { Message = "Appointment updated successfully", AppointmentId = existingAppointment.AppointmentId });
        }

        /// <summary>
        /// Deletes an appointment by ID.
        /// </summary>
        /// <param name="id">Appointment ID.</param>
        /// <returns>No content on success.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteAppointment(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
            if (appointment == null)
                return NotFound(new { Message = "Appointment not found" });

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
