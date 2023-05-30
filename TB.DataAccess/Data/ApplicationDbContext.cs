


using Microsoft.EntityFrameworkCore;
using TB.DataAccess.Models;

namespace TB.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Event> Events { get; set; }
       
    }
}


