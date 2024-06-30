using Microsoft.AspNetCore.Mvc;
using VetClinic.Models;
using VetClinic.ViewModels;
using System;
using Microsoft.AspNetCore.Authorization;

namespace VetClinic.Controllers
{
    [Authorize(Policy = "Admin")]
    public class RoomsController : Controller
    {
        private readonly VetClinicContext _context;

        public RoomsController(VetClinicContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                var rooms = RoomsRepository.GetRooms(_context);
                return View(rooms);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Index: {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while retrieving rooms." });
            }
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";
            return View();
        }

        [HttpPost]
        public IActionResult Add(Room room)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RoomsRepository.AddRoom(_context, room);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Action = "add";
                return View(room);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Add (POST): {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while adding the room." });
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Action = "edit";
                var room = RoomsRepository.GetRoomById(_context, id);
                if (room == null)
                {
                    return NotFound();
                }

                return View(room);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit (GET): {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while retrieving the room for editing." });
            }
        }

        [HttpPost]
        public IActionResult Edit(Room room)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RoomsRepository.UpdateRoom(_context, room.RoomId, room);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Action = "edit";
                return View(room);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit (POST): {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while updating the room." });
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                RoomsRepository.DeleteRoom(_context, id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Delete: {ex.Message}");
                return View("Error", new ErrorViewModel { ErrorMessage = "An error occurred while deleting the room." });
            }
        }
    }
}
