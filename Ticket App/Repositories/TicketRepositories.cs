using Microsoft.EntityFrameworkCore;
using Ticket_App.Context;
using Ticket_App.Dto;
using Ticket_App.Model;
using Ticket_App.Repositories.interfaces;

namespace Ticket_App.Repositories
{
    public class TicketRepositories : ITicketRepositories
    {
        private readonly UserContext context;
        public TicketRepositories(UserContext _context) 
        { 
         context = _context;
        }

        public async Task<Tickets?> FindTicketById(Guid ticketId)
        {
            return await context.Tickets.FirstOrDefaultAsync(t =>t.Id == ticketId);
        }

        public async Task<Users?> GetUserById(Guid Id)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Id == Id);

        }

        public async Task<Guid> UserBuyedTicketEvent(Users users, Tickets tickets)
        {
            try
            {
                var userTicket = new UserTickets
                {
                    TicketId = tickets.Id ,
                    UsersId =users.Id,
                    User = users,
                    Ticket = tickets, 
                    
                };
                
                context.UserTickets.Add(userTicket);
                
                await context.SaveChangesAsync();

                return userTicket.UsersId;
            }
            catch(Exception ex) {
                Console.WriteLine(ex.InnerException);
                throw new Exception(ex.Message);
            }
        }
       
    }
}
