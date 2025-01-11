using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetClinic.Models;
using System.Collections.Generic;
using System.Linq;

namespace VetClinic.Areas.Api.Controllers
{
    [Route("Api/Patients")]
    [ApiController]
    public class PatientsApiController : ControllerBase
    {
        private readonly VetClinicContext _context;

        public PatientsApiController(VetClinicContext context)
        {
            _context = context;
        }

        // GET: api/patients
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            var patients = _context.Patients
                .Include(p => p.Doctor) 
                .Include(p => p.Appointments) 
                .ToList();

            return Ok(patients);
        }

        // GET: api/patients/{id}
        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatientById(int id)
        {
            var patient = _context.Patients
                .Include(p => p.Doctor)
                .Include(p => p.Appointments)
                .FirstOrDefault(p => p.PatientId == id);

            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        // POST: api/patients
        [HttpPost]
        public ActionResult<Patient> CreatePatient([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Patients.Add(patient);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPatientById), new { id = patient.PatientId }, patient);
        }

        // PUT: api/patients/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePatient(int id, [FromBody] Patient patient)
        {
            if (id != patient.PatientId || !ModelState.IsValid)
                return BadRequest();

            var existingPatient = _context.Patients.FirstOrDefault(p => p.PatientId == id);
            if (existingPatient == null)
                return NotFound();

            existingPatient.DoctorId = patient.DoctorId;
            existingPatient.PatientName = patient.PatientName;
            existingPatient.Type = patient.Type;
            existingPatient.Breed = patient.Breed;
            existingPatient.Age = patient.Age;
            existingPatient.OwnerInfo = patient.OwnerInfo;
            existingPatient.Diagnosis = patient.Diagnosis;

            _context.SaveChanges();
            return Ok(existingPatient);
        }

        // DELETE: api/patients/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            var patient = _context.Patients
                .Include(p => p.Appointments)
                .FirstOrDefault(p => p.PatientId == id);

            if (patient == null)
                return NotFound();

            if (patient.Appointments != null && patient.Appointments.Any())
            {
                _context.Appointments.RemoveRange(patient.Appointments);
            }

            _context.Patients.Remove(patient);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
