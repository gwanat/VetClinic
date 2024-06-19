using Microsoft.AspNetCore.Mvc;
using VetClinic.Models;
using VetClinic.ViewModels;

namespace VetClinic.Controllers
{
    public class PatientsController : Controller
    {
        public IActionResult Index()
        {
            var patients = PatientsRepository.GetPatients(loadPet: true);
            return View(patients);
        }


        public IActionResult Add()
        {
            ViewBag.Action = "add";

            var patientViewModel = new PatientViewModel
            {
                Pets = PetsRepository.GetPets()
            };

            return View(patientViewModel);
        }

        [HttpPost]
        public IActionResult Add(PatientViewModel patientViewModel)
        {
            if (ModelState.IsValid)
            {
                PatientsRepository.AddPatient(patientViewModel.Patient);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "add";
            patientViewModel.Pets = PetsRepository.GetPets();
            return View(patientViewModel);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "edit";
            var patientViewModel = new PatientViewModel
            {
                Patient = PatientsRepository.GetPatientById(id) ?? new Patient(),
                Pets = PetsRepository.GetPets()
            };

            return View(patientViewModel);
        }

        [HttpPost]
        public IActionResult Edit(PatientViewModel patientViewModel)
        {
            if (ModelState.IsValid)
            {
                PatientsRepository.UpdatePatient(patientViewModel.Patient.PatientId, patientViewModel.Patient);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "edit";
            patientViewModel.Pets = PetsRepository.GetPets();
            return View(patientViewModel);
        }

        public IActionResult Delete(int id)
        {
            PatientsRepository.DeletePatient(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
