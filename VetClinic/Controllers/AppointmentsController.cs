using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VetClinic.Models;
using System.Linq;

namespace VetClinic.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly VetClinicContext _context;

        public AppointmentsController(VetClinicContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var appointments = AppointmentsRepository.GetAppointments(loadRelated: true, context: _context);
            return View(appointments);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Action = "edit";
            var appointment = AppointmentsRepository.GetAppointmentById(id, loadRelated: true, context: _context);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.PatientName = appointment.Patient?.PatientName;
            PopulateDropDowns();
            return View(appointment);
        }

        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            if (appointment.EndTime <= appointment.StartTime)
            {
                ModelState.AddModelError("EndTime", "End time must be later than start time");
            }

            if (ModelState.IsValid)
            {
                var patient = _context.Patients.FirstOrDefault(p => p.PatientName == appointment.PatientName);
                if (patient == null)
                {
                    ModelState.AddModelError("PatientName", "Patient not found");
                    PopulateDropDowns();
                    return View(appointment);
                }

                appointment.PatientId = patient.PatientId;
                AppointmentsRepository.UpdateAppointment(appointment.AppointmentId, appointment, _context);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "edit";
            PopulateDropDowns();
            return View(appointment);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";
            PopulateDropDowns();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Appointment appointment)
        {
            if (appointment.EndTime <= appointment.StartTime)
            {
                ModelState.AddModelError("EndTime", "End time must be later than start time");
            }

            if (ModelState.IsValid)
            {
                var patient = _context.Patients.FirstOrDefault(p => p.PatientName == appointment.PatientName);
                if (patient == null)
                {
                    ModelState.AddModelError("PatientName", "Patient not found");
                    PopulateDropDowns();
                    return View(appointment);
                }

                appointment.PatientId = patient.PatientId;
                AppointmentsRepository.AddAppointment(appointment, _context);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "add";
            PopulateDropDowns();
            return View(appointment);
        }

        public IActionResult Delete(int id)
        {
            AppointmentsRepository.DeleteAppointment(id, _context);
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropDowns()
        {
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorId", "Name");
            ViewBag.Rooms = new SelectList(_context.Rooms, "RoomId", "RoomNumber");
        }
    }
}
