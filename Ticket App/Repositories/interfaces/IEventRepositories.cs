using Ticket_App.Dto;
using Ticket_App.Model;

namespace Ticket_App.Repositories.interfaces
{
    public interface IEventRepositories
    {
        public Task<Events> CreateEvent(EventsDto eventsDto);
        public Task<Guid> CreateTicketAtEvent(TicketsDto ticketsDto, Events events);
        public Task<Users?> GetUserById(Guid Id);

    }
}
