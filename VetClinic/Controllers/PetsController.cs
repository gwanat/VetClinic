using Microsoft.AspNetCore.Mvc;
using VetClinic.Models;

namespace VetClinic.Controllers
{
    public class PetsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            var pet = new Pet { PetId = id.HasValue ? id.Value : 0 };

            return View(pet);
        }

    }
}
