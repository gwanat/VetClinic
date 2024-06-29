using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VetClinic.Models;
using System;
using System.Linq;
using VetClinic.ViewModels;

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
            try
            {
                var appointments = AppointmentsRepository.GetAppointments(loadRelated: true, context: _context);
                return View(appointments);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Index: {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while retrieving appointments." });
            }
        }

        public IActionResult Edit(int id)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit (GET): {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while preparing the edit appointment form." });
            }
        }

        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit (POST): {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while updating the appointment." });
            }
        }

        public IActionResult Add()
        {
            try
            {
                ViewBag.Action = "add";
                PopulateDropDowns();
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Add (GET): {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while preparing the add appointment form." });
            }
        }

        [HttpPost]
        public IActionResult Add(Appointment appointment)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Add (POST): {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while adding the appointment." });
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                AppointmentsRepository.DeleteAppointment(id, _context);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Delete: {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while deleting the appointment." });
            }
        }

        private void PopulateDropDowns()
        {
            try
            {
                ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorId", "Name");
                ViewBag.Rooms = new SelectList(_context.Rooms, "RoomId", "RoomNumber");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PopulateDropDowns: {ex.Message}");
                throw;
            }
        }
    }
}
