﻿using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Resturant.Models;

namespace Resturant.Controllers
{
    public class BookingController : Controller
    {
        public BookingController(ResturantContext context) {
            _context = context;
        }

        private readonly ResturantContext _context;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult BookingCalendar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Done(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return View("Done", booking);    
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            Booking booking = _context.Bookings?.Find(id) ?? new Booking();
            return View(booking);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Save(Booking newBooking, String EditBtn)
        {
            if(EditBtn == "submit")
            {
                EntityEntry<Booking> entityEntry = _context.Bookings.Entry(newBooking);
                entityEntry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else if(EditBtn == "delete")
            {
                _context.Remove(newBooking);
            }
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        [Authorize]
        public IActionResult List()
        {
            List<Booking> listBookings = _context.Bookings.ToList();
            return View(listBookings);
        }

        public JsonResult GetBookings()
        {
            var bookings = _context.Bookings.ToList();
            return Json(bookings);
        }

        [HttpGet]
        public JsonResult GetBookingCounts(int year, int month)
        {
            var bookingCounts = _context.Bookings
                .Where(b => b.From.Year == year && b.From.Month == month)
                .GroupBy(b => b.From.Date)
                .Select(group => new { Date = group.Key, Count = group.Count() })
                .ToList();

            return Json(bookingCounts);
        }
    }
}
