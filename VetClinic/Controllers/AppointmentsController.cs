using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VetClinic.Models;
using System.Linq;

namespace VetClinic.Controllers
{
    public class AppointmentsController : Controller
    {
        public IActionResult Index()
        {
            var appointments = AppointmentsRepository.GetAppointments(loadRelated: true);
            return View(appointments);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Action = "edit";
            var appointment = AppointmentsRepository.GetAppointmentById(id, loadRelated: true);
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
            if (ModelState.IsValid)
            {
                var patient = PatientsRepository.GetPatients().FirstOrDefault(p => p.PatientName == appointment.PatientName);
                if (patient == null)
                {
                    ModelState.AddModelError("PatientName", "Patient not found");
                    PopulateDropDowns();
                    return View(appointment);
                }

                appointment.PatientId = patient.PatientId;
                AppointmentsRepository.UpdateAppointment(appointment.AppointmentId, appointment);
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
            if (ModelState.IsValid)
            {
                var patient = PatientsRepository.GetPatients().FirstOrDefault(p => p.PatientName == appointment.PatientName);
                if (patient == null)
                {
                    ModelState.AddModelError("PatientName", "Patient not found");
                    PopulateDropDowns();
                    return View(appointment);
                }

                appointment.PatientId = patient.PatientId;
                AppointmentsRepository.AddAppointment(appointment);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "add";
            PopulateDropDowns();
            return View(appointment);
        }

        public IActionResult Delete(int id)
        {
            AppointmentsRepository.DeleteAppointment(id);
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropDowns()
        {
            ViewBag.Doctors = new SelectList(DoctorsRepository.GetDoctors(), "DoctorId", "Name");
            ViewBag.Rooms = new SelectList(RoomsRepository.GetRooms(), "RoomId", "RoomNumber");
        }
    }
}
