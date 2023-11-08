using Microsoft.EntityFrameworkCore;

namespace Resturant.Models
{

    public class ResturantContext : DbContext
    {

        public ResturantContext(DbContextOptions<ResturantContext> options) : base(options) 
        { 
        
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Booking> Bookings { get; set; }

    }
}