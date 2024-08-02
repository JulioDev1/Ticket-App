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

        public async Task<Guid> UserBuyedTicketEvent(Guid userId, Guid tickedId)
        {
            var userTicket = new UserTickets
            {
                TicketId = tickedId,
                UsersId = userId
            };
           context.UserTickets.Add(userTicket);
           await context.SaveChangesAsync();
           
           return userTicket.UsersId;
        }
       
    }
}
