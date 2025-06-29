using AvinashBackEndAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AvinashBackEndAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ScheduleTransfer> ScheduleTransfers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.ScheduleTransfers)
                .WithOne(st => st.User)
                .HasForeignKey(st => st.UserID);



            modelBuilder.Entity<Account>()
                .HasMany(a => a.DebitTransactions)
                .WithOne(t => t.DebitAccount)
                .HasForeignKey(t => t.DebitAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.CreditTransactions)
                .WithOne(t => t.CreditAccount)
                .HasForeignKey(t => t.CreditAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
