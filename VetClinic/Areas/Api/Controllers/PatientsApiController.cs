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

        /// <summary>
        /// Retrieves all patients.
        /// </summary>
        /// <returns>List of patients.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            var patients = _context.Patients
                .Include(p => p.Doctor)
                .Include(p => p.Appointments)
                .ToList();

            return Ok(patients);
        }

        /// <summary>
        /// Retrieves a patient by ID.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <returns>The patient details.</returns>
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

        /// <summary>
        /// Creates a new patient.
        /// </summary>
        /// <param name="patient">Patient object to create.</param>
        /// <returns>The created patient.</returns>
        [HttpPost]
        public ActionResult<Patient> CreatePatient([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Patients.Add(patient);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPatientById), new { id = patient.PatientId }, patient);
        }

        /// <summary>
        /// Updates an existing patient.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <param name="patient">Updated patient object.</param>
        /// <returns>The updated patient.</returns>
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

        /// <summary>
        /// Deletes a patient by ID.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <returns>No content if deletion is successful.</returns>
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
