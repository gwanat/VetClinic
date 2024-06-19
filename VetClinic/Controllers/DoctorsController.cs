using Microsoft.AspNetCore.Mvc;
using VetClinic.Models;

namespace VetClinic.Controllers
{
    public class DoctorsController : Controller
    {
        public IActionResult Index()
        {
            var doctors = DoctorsRepository.GetDoctors();
            return View(doctors);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";

            var doctor = DoctorsRepository.GetDoctorById(id.HasValue ? id.Value : 0);

            return View(doctor);
        }

        [HttpPost]
        public IActionResult Edit(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                DoctorsRepository.UpdateDoctor(doctor.DoctorId, doctor);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "edit";
            return View(doctor);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";

            return View();
        }

        [HttpPost]
        public IActionResult Add(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                DoctorsRepository.AddDoctor(doctor);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "add";
            return View(doctor);
        }

        public IActionResult Delete(int doctorId)
        {
            DoctorsRepository.DeleteDoctor(doctorId);
            return RedirectToAction(nameof(Index));
        }
    }
}