using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetClinic.Models;
using System.Collections.Generic;
using System.Linq;

namespace VetClinic.Areas.Api.Controllers
{
    [Route("Api/Doctors")]
    [ApiController]
    public class DoctorsApiController : ControllerBase
    {
        private readonly VetClinicContext _context;

        public DoctorsApiController(VetClinicContext context)
        {
            _context = context;
        }

        // GET: api/doctors
        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> GetDoctors()
        {
            var doctors = _context.Doctors.ToList();
            return Ok(doctors);
        }

        // GET: api/doctors/{id}
        [HttpGet("{id}")]
        public ActionResult<Doctor> GetDoctorById(int id)
        {
            var doctor = _context.Doctors
                .Include(d => d.Patients) 
                .Include(d => d.Appointments) 
                .FirstOrDefault(d => d.DoctorId == id);

            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        // POST: api/doctors
        [HttpPost]
        public ActionResult<Doctor> CreateDoctor([FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.DoctorId }, doctor);
        }

        // PUT: api/doctors/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateDoctor(int id, [FromBody] Doctor doctor)
        {
            if (id != doctor.DoctorId || !ModelState.IsValid)
                return BadRequest();

            var existingDoctor = _context.Doctors.FirstOrDefault(d => d.DoctorId == id);
            if (existingDoctor == null)
                return NotFound();

            existingDoctor.Name = doctor.Name;
            existingDoctor.Specialty = doctor.Specialty;

            _context.SaveChanges();
            return Ok(existingDoctor);
        }

        // DELETE: api/doctors/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            var doctor = _context.Doctors
                .Include(d => d.Appointments)
                .FirstOrDefault(d => d.DoctorId == id);

            if (doctor == null)
                return NotFound();

            if (doctor.Appointments != null && doctor.Appointments.Any())
            {
                return Conflict(new { message = "Cannot delete the doctor because they have associated appointments." });
            }

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
