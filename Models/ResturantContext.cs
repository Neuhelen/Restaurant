using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Resturant.Models
{
    public class ResturantContext : IdentityDbContext
    {

        public ResturantContext(DbContextOptions<ResturantContext> options) : base(options)
        {

        }
        
        public DbSet<Booking>? Bookings { get; set; }

    }
}