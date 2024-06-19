using Microsoft.AspNetCore.Mvc;
using VetClinic.Models;

namespace VetClinic.Controllers
{
    public class PetsController : Controller
    {
        public IActionResult Index()
        {
            var pets = PetsRepository.GetPets();
            return View(pets);
        }


        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";

            var pet = PetsRepository.GetPetById(id.HasValue ? id.Value : 0 );

            return View(pet);
        }

        [HttpPost]
        public IActionResult Edit(Pet pet)
        {
            if (ModelState.IsValid)
            {
                PetsRepository.UpdatePet(pet.PetId, pet);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "edit";
            return View(pet);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";

            return View();
        }

        [HttpPost]
        public IActionResult Add(Pet pet)
        {
            if (ModelState.IsValid)
            {
                PetsRepository.AddPet(pet);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "add";
            return View(pet);
        }

        public IActionResult Delete(int petId)
        {
            PetsRepository.DeletePet(petId);
            return RedirectToAction(nameof(Index));
        }
    }
}
