using Ticket_App.Controllers.Dto;
using Ticket_App.Dto;
using Ticket_App.Model;

namespace Ticket_App.Service.Interface
{
    public interface IEventsService
    {
        Task<Guid> CreateEvent(EventsDto eventsDto, TicketsDto ticketsDto);
        public Task<Users?> GetUserId(Guid userId);

        Task<Events> UserUpdateYourEvent( Events events);
        Task<Events?> GetEventById(Guid eventId, Guid userId);
        Task<List<Events>> GetAllEventsCreatedByUser(Guid Id);
        Task<bool> DeleteUserEvent(Guid eventId, Guid userId);
        Task<List<Events>> ListAllTickets();


    }
}
