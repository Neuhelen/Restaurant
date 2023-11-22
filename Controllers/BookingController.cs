using Microsoft.AspNetCore.Mvc;

namespace Resturant.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult Done()
        {
            return View();
        }
    }
}
