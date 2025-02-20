﻿using Microsoft.AspNetCore.Mvc;
using Resturant.Models;
using System.Data;
using System.Diagnostics;

using System.Linq;
using System.Xml.Linq;

namespace Resturant.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Menu()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private class Test
        {
            public int Idtest { get; set; }
            public string Test_ { get; set; }
            public int Number { get; set; }
            public bool Truefalse { get; set; }

            public Test(int idtest, string test, int number, bool truefalse)
            {
                Idtest = idtest;
                Test_ = test;
                Number = number;
                Truefalse = truefalse;
            }
        }
        public ActionResult ToggleDarkMode()
        {
            //This if-statement checks whether the dark mode cookie exists, and if it is set to true, 
            //thus enabling dark mode. 
            if (Request.Cookies["DarkMode"] == "true")
            {
                //This function disables dark mode, if it is enabled. 
                DisableDarkMode();
            }
            else
            {
                //This function enables dark mode, if it is disabled. 
                EnableDarkMode();
            }

            //The part redirects the user back to index. 
            return RedirectToAction("Index");
        }
        private void EnableDarkMode()
        {
            //This part creates the DarkMode coókie, sets the expiration date to 30 days, 
            //and specifies that the cookie is used for the whole website.
            Response.Cookies.Append("DarkMode", "true", new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(30),
                Domain = Request.Host.Host,
                Path = "/"
            });
        }

        private void DisableDarkMode()
        {
            //This part deletes the DarkMode cookie in order to disable the dark mode. 
            Response.Cookies.Delete("DarkMode", new CookieOptions
            {
                Domain = Request.Host.Host,
                Path = "/"
            });
        }

    }
}