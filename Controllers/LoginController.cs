using Microsoft.AspNetCore.Mvc;
using Resturant.Models;

namespace Resturant.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public bool Validate(string user, string password)
        {
            bool valid = false;
            

            if (valid)
            {
                return true;
            }
            else
            {    
                return false;
            }

        }

    }

    

}
