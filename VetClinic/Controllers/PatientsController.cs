using Microsoft.AspNetCore.Mvc;
using VetClinic.Models;
using VetClinic.ViewModels;

namespace VetClinic.Controllers
{
    public class PatientsController : Controller
    {
        private readonly VetClinicContext _context;

        public PatientsController(VetClinicContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchPatientName, string searchOwnerName)
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

        public IActionResult Add()
        {
            ViewBag.Action = "add";

            var patientViewModel = new PatientViewModel
            {
                Doctors = DoctorsRepository.GetDoctors(_context)
            };

            return View(patientViewModel);
        }

        [HttpPost]
        public IActionResult Add(PatientViewModel patientViewModel)
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

        public IActionResult Edit(int id)
        {
            ViewBag.Action = "edit";
            var patientViewModel = new PatientViewModel
            {
                Patient = PatientsRepository.GetPatientById(_context, id) ?? new Patient(),
                Doctors = DoctorsRepository.GetDoctors(_context)
            };

            return View(patientViewModel);
        }

        [HttpPost]
        public IActionResult Edit(PatientViewModel patientViewModel)
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

        public IActionResult Delete(int id)
        {
            PatientsRepository.DeletePatient(_context, id);
            return RedirectToAction(nameof(Index));
        }
    }
}
