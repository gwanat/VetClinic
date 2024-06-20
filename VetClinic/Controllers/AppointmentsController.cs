using Microsoft.AspNetCore.Mvc;
using VetClinic.Models;
using System;
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
            return View(appointment);
        }


        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                AppointmentsRepository.UpdateAppointment(appointment.AppointmentId, appointment);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "edit";
            return View(appointment);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";

            return View();
        }

        [HttpPost]
        public IActionResult Add(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                AppointmentsRepository.AddAppointment(appointment);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "add";
            return View(appointment);
        }

        public IActionResult Delete(int id)
        {
            AppointmentsRepository.DeleteAppointment(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
