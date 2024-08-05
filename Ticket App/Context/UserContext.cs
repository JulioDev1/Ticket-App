using Microsoft.EntityFrameworkCore;
using Ticket_App.Model;

namespace Ticket_App.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Events> Events { get; set; }

        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<UserTickets> UserTickets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTickets>().HasKey(ut => ut.Id);
            modelBuilder.Entity<UserTickets>()
                .HasOne(up => up.User)
                .WithMany(t => t.userTickets)
                .HasForeignKey(u=> u.UsersId);

            modelBuilder.Entity<UserTickets>()
                .HasOne(up => up.Ticket)
                .WithMany(t => t.userTickets)
                .HasForeignKey(u => u.TicketId);
        }
    }
}
