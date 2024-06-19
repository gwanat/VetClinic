using Microsoft.AspNetCore.Mvc;
using VetClinic.Models;

namespace VetClinic.Controllers
{
    public class PatientsController : Controller
    {
        public IActionResult Index()
        {
            var patients = PatientsRepository.GetPatients(loadPet: true);
            return View();
        }
    }
}
