using Microsoft.AspNetCore.Mvc;

namespace VetClinic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
