using Ticket_App.Dto;
using Ticket_App.Model;

namespace Ticket_App.Repositories.interfaces
{
    public interface ITicketRepositories
    {
        public Task<Guid> UserBuyedTicketEvent(Users users, Tickets tickets);
        public Task<Users?> GetUserById(Guid Id);

        public Task<Tickets?> FindTicketById(Guid ticketId);
    }
}
