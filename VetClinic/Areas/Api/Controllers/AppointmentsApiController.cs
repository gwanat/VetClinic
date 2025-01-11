using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetClinic.Models;
using System.Collections.Generic;
using System.Linq;

namespace VetClinic.Areas.Api.Controllers
{
    [Route("Api/Appointments")]
    [ApiController]
    public class AppointmentsApiController : ControllerBase
    {
        private readonly VetClinicContext _context;

        public AppointmentsApiController(VetClinicContext context)
        {
            _context = context;
        }

        // GET: api/appointments
        [HttpGet]
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
                    DoctorName = a.Doctor != null ? a.Doctor.Name : string.Empty, 
                    PatientName = a.PatientName, 
                    ClientName = a.ClientName,  
                    RoomNumber = a.Room != null ? a.Room.RoomNumber.ToString() : "N/A"
                })
                .ToList();

            return Ok(appointments);
        }

        // GET: api/appointments/{id}
        [HttpGet("{id}")]
        public ActionResult<object> GetAppointmentById(int id)
        {
            var appointment = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.Room)
                .FirstOrDefault(a => a.AppointmentId == id);

            if (appointment == null)
                return NotFound();

            return Ok(new
            {
                appointment.AppointmentId,
                appointment.StartTime,
                appointment.EndTime,
                DoctorName = appointment.Doctor?.Name, 
                PatientName = appointment.PatientName, 
                ClientName = appointment.ClientName,   
                RoomNumber = appointment.Room?.RoomNumber
            });
        }

        // POST: api/appointments
        [HttpPost]
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
                return Conflict("The appointment overlaps with an existing appointment.");

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return Ok(new
            {
                Message = "Appointment created successfully",
                AppointmentId = appointment.AppointmentId
            });
        }

        // PUT: api/appointments/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateAppointment(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.AppointmentId || !ModelState.IsValid)
                return BadRequest();

            var existingAppointment = _context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
            if (existingAppointment == null)
                return NotFound();

            var hasConflict = _context.Appointments.Any(a =>
                a.AppointmentId != id && 
                ((a.RoomId == appointment.RoomId &&
                 ((appointment.StartTime >= a.StartTime && appointment.StartTime < a.EndTime) ||
                  (appointment.EndTime > a.StartTime && appointment.EndTime <= a.EndTime))) ||
                (a.DoctorId == appointment.DoctorId &&
                 ((appointment.StartTime >= a.StartTime && appointment.StartTime < a.EndTime) ||
                  (appointment.EndTime > a.StartTime && appointment.EndTime <= a.EndTime)))));

            if (hasConflict)
                return Conflict("The appointment overlaps with an existing appointment.");

            existingAppointment.StartTime = appointment.StartTime;
            existingAppointment.EndTime = appointment.EndTime;
            existingAppointment.DoctorId = appointment.DoctorId;
            existingAppointment.PatientId = appointment.PatientId;
            existingAppointment.RoomId = appointment.RoomId;

            _context.SaveChanges();

            return Ok(new
            {
                Message = "Appointment updated successfully",
                AppointmentId = existingAppointment.AppointmentId
            });
        }

        // DELETE: api/appointments/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
            if (appointment == null)
                return NotFound();

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
