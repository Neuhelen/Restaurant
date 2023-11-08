using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resturant.Identity;
using Resturant.Models;
using System.Net;
using System.Security.Cryptography;
using System.Text;

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

        public IActionResult Register(RegisterViewModel vmUser)
        {
            if (vmUser.UserName == null)
                return View("Index", vmUser);
            // Default UserStore constructor uses the default connection string named: DefaultConnection
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser() { 
                UserName = vmUser.UserName,
                PasswordHash = SHA256.HashData(Encoding.UTF8.GetBytes(vmUser.Password)).ToString()
            };

            IdentityResult result = manager.Create(user);

            vmUser.Result = result;

            if (result != null && result.Succeeded)
            {
                return View("Index", vmUser);
                //StatusMessage.Text = string.Format("User {0} was created successfully!", user.UserName);
            }
            else
            {
                return View("Index", vmUser);
                //StatusMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        [Authorize(Roles = "")]
        public string Check()
        {
            return "Test";
        }
    }

}
