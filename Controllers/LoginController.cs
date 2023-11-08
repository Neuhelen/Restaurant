using Microsoft.AspNetCore.Mvc;
using Resturant.Models;
using System.Net;

namespace Resturant.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/Login/Validate")]
        [ActionName("Validate")]
        public IActionResult Validate(LoginViewModel login)
        {
            bool valid = false;

            if (valid)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return View();
            }

        }

    }

}
