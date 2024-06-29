using Microsoft.AspNetCore.Mvc;
using VetClinic.Models;

namespace VetClinic.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly VetClinicContext _context;

        public DoctorsController(VetClinicContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var doctors = DoctorsRepository.GetDoctors(_context);
            return View(doctors);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";

            var doctor = DoctorsRepository.GetDoctorById(_context, id.HasValue ? id.Value : 0);

            return View(doctor);
        }

        [HttpPost]
        public IActionResult Edit(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                DoctorsRepository.UpdateDoctor(_context, doctor.DoctorId, doctor);
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
                DoctorsRepository.AddDoctor(_context, doctor);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "add";
            return View(doctor);
        }

        public IActionResult Delete(int doctorId)
        {
            DoctorsRepository.DeleteDoctor(_context, doctorId);
            return RedirectToAction(nameof(Index));
        }
    }
}
