using Microsoft.EntityFrameworkCore;
using Ticket_App.Model;

namespace Ticket_App.Context
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext>options):base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
