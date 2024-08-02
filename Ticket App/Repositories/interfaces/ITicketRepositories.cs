using Ticket_App.Model;

namespace Ticket_App.Repositories.interfaces
{
    public interface ITicketRepositories
    {
        public Task<Guid> UserBuyedTicketEvent(Guid userId, Guid tickedId);
        public Task<Tickets?> FindTicketById(Guid ticketId);
    }
}
