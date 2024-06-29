using Microsoft.AspNetCore.Mvc;
using VetClinic.Models;
using VetClinic.ViewModels;
using System;
using Microsoft.AspNetCore.Authorization;

namespace VetClinic.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        private readonly VetClinicContext _context;

        public DoctorsController(VetClinicContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                var doctors = DoctorsRepository.GetDoctors(_context);
                return View(doctors);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Index: {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while retrieving doctors." });
            }
        }

        public IActionResult Edit(int? id)
        {
            try
            {
                ViewBag.Action = "edit";
                var doctor = DoctorsRepository.GetDoctorById(_context, id.HasValue ? id.Value : 0);
                return View(doctor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit (GET): {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while preparing the edit doctor form." });
            }
        }

        [HttpPost]
        public IActionResult Edit(Doctor doctor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DoctorsRepository.UpdateDoctor(_context, doctor.DoctorId, doctor);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Action = "edit";
                return View(doctor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit (POST): {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while updating the doctor." });
            }
        }

        public IActionResult Add()
        {
            try
            {
                ViewBag.Action = "add";
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Add (GET): {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while preparing the add doctor form." });
            }
        }

        [HttpPost]
        public IActionResult Add(Doctor doctor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DoctorsRepository.AddDoctor(_context, doctor);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Action = "add";
                return View(doctor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Add (POST): {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while adding the doctor." });
            }
        }

        public IActionResult Delete(int doctorId)
        {
            try
            {
                DoctorsRepository.DeleteDoctor(_context, doctorId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Delete: {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while deleting the doctor." });
            }
        }
    }
}
