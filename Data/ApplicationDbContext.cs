using CarRentalApplication_API.Model;
using Microsoft.EntityFrameworkCore;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Vehicles> Vehicles { get; set; }
    public DbSet<SystemLog> SystemLogs { get; set; }
    public DbSet<vehicle_categories> vehicle_Categories { get; set; }

    // Prices table
    public DbSet<Price> Prices { get; set; }
}
