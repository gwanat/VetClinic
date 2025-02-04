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

        /// <summary>
        /// Retrieves all doctors.
        /// </summary>
        /// <returns>List of doctors.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> GetDoctors()
        {
            var doctors = _context.Doctors.ToList();
            return Ok(doctors);
        }

        /// <summary>
        /// Retrieves a doctor by ID.
        /// </summary>
        /// <param name="id">The ID of the doctor.</param>
        /// <returns>The doctor details.</returns>
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

        /// <summary>
        /// Creates a new doctor.
        /// </summary>
        /// <param name="doctor">Doctor object to create.</param>
        /// <returns>The created doctor.</returns>
        [HttpPost]
        public ActionResult<Doctor> CreateDoctor([FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.DoctorId }, doctor);
        }

        /// <summary>
        /// Updates an existing doctor.
        /// </summary>
        /// <param name="id">The ID of the doctor.</param>
        /// <param name="doctor">Updated doctor object.</param>
        /// <returns>The updated doctor.</returns>
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

        /// <summary>
        /// Deletes a doctor by ID.
        /// </summary>
        /// <param name="id">The ID of the doctor.</param>
        /// <returns>No content if deletion is successful.</returns>
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
