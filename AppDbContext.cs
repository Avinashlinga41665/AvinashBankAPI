using AvinashBackEndAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AvinashBackEndAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Login> Users { get; set; }

        public DbSet<Register> Registrations { get; set; }

    }
}
