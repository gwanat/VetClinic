using Microsoft.AspNetCore.Mvc;
using VetClinic.Models;
using VetClinic.ViewModels;
using System;
using Microsoft.AspNetCore.Authorization;

namespace VetClinic.Controllers
{
    [Authorize(Policy ="AdminOrDoctor")]
    public class PatientsController : Controller
    {
        private readonly VetClinicContext _context;

        public PatientsController(VetClinicContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchPatientName, string searchOwnerName)
        {
            try
            {
                var patients = PatientsRepository.GetPatients(_context, loadDoctor: true);

                if (!string.IsNullOrEmpty(searchPatientName))
                {
                    patients = patients.Where(p => p.PatientName.Contains(searchPatientName, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(searchOwnerName))
                {
                    patients = patients.Where(p => p.OwnerInfo.Contains(searchOwnerName, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                ViewBag.SearchPatientName = searchPatientName;
                ViewBag.SearchOwnerName = searchOwnerName;

                return View(patients);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Index: {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while retrieving patients." });
            }
        }

        public IActionResult Add()
        {
            try
            {
                ViewBag.Action = "add";

                var patientViewModel = new PatientViewModel
                {
                    Doctors = DoctorsRepository.GetDoctors(_context)
                };

                return View(patientViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Add (GET): {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while preparing the add patient form." });
            }
        }

        [HttpPost]
        public IActionResult Add(PatientViewModel patientViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PatientsRepository.AddPatient(_context, patientViewModel.Patient);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Action = "add";
                patientViewModel.Doctors = DoctorsRepository.GetDoctors(_context);
                return View(patientViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Add (POST): {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while adding the patient." });
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Action = "edit";
                var patientViewModel = new PatientViewModel
                {
                    Patient = PatientsRepository.GetPatientById(_context, id) ?? new Patient(),
                    Doctors = DoctorsRepository.GetDoctors(_context)
                };

                return View(patientViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit (GET): {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while preparing the edit patient form." });
            }
        }

        [HttpPost]
        public IActionResult Edit(PatientViewModel patientViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PatientsRepository.UpdatePatient(_context, patientViewModel.Patient.PatientId, patientViewModel.Patient);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Action = "edit";
                patientViewModel.Doctors = DoctorsRepository.GetDoctors(_context);
                return View(patientViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit (POST): {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while updating the patient." });
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                PatientsRepository.DeletePatient(_context, id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Delete: {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while deleting the patient." });
            }
        }
    }
}
