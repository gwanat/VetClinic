using Microsoft.AspNetCore.Mvc;
using VetClinic.Models;
using VetClinic.ViewModels;


namespace VetClinic.Controllers
{
    public class PatientsController : Controller
    {
        public IActionResult Index(string searchPatientName, string searchOwnerName)
        {
            var patients = PatientsRepository.GetPatients(loadDoctor: true);

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

        public IActionResult Add()
        {
            ViewBag.Action = "add";

            var patientViewModel = new PatientViewModel
            {
                Doctors = DoctorsRepository.GetDoctors()
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
            patientViewModel.Doctors = DoctorsRepository.GetDoctors();
            return View(patientViewModel);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Action = "edit";
            var patientViewModel = new PatientViewModel
            {
                Patient = PatientsRepository.GetPatientById(id) ?? new Patient(),
                Doctors = DoctorsRepository.GetDoctors()
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
            patientViewModel.Doctors = DoctorsRepository.GetDoctors();
            return View(patientViewModel);
        }

        public IActionResult Delete(int id)
        {
            PatientsRepository.DeletePatient(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
