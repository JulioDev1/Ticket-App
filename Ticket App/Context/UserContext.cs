using Microsoft.EntityFrameworkCore;
using Ticket_App.Model;

namespace Ticket_App.Context
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext>options):base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Events> Events {  get; set; }  
        
        public DbSet <Tickets> Tickets { get; set; }
        public DbSet<UserTickets> UserTickets { get; set; }


    }
}
