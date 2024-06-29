using Microsoft.AspNetCore.Mvc;
using VetClinic.Models;

namespace VetClinic.Controllers
{
    public class RoomsController : Controller
    {
        private readonly VetClinicContext _context;

        public RoomsController(VetClinicContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var rooms = RoomsRepository.GetRooms(_context);
            return View(rooms);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";
            return View();
        }

        [HttpPost]
        public IActionResult Add(Room room)
        {
            if (ModelState.IsValid)
            {
                RoomsRepository.AddRoom(_context, room);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "add";
            return View(room);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Action = "edit";
            var room = RoomsRepository.GetRoomById(_context, id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpPost]
        public IActionResult Edit(Room room)
        {
            if (ModelState.IsValid)
            {
                RoomsRepository.UpdateRoom(_context, room.RoomId, room);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "edit";
            return View(room);
        }

        public IActionResult Delete(int id)
        {
            RoomsRepository.DeleteRoom(_context, id);
            return RedirectToAction(nameof(Index));
        }
    }
}
