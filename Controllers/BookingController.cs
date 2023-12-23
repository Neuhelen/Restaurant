using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Save(Booking newBooking)
        {
            EntityEntry<Booking> entityEntry = _context.Bookings.Entry(newBooking);
            entityEntry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        [Authorize]
        public IActionResult List()
        {
            List<Booking> listBookings = _context.Bookings.ToList();
            return View(listBookings);
        }
    }
}
