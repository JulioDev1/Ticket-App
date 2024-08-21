using Ticket_App.Controllers.Dto;
using Ticket_App.Dto;
using Ticket_App.Model;

namespace Ticket_App.Service.Interface
{
    public interface IEventsService
    {
        Task<Guid> CreateEvent(EventsDto eventsDto, TicketsDto ticketsDto);
        public Task<Users?> GetUserId(Guid userId);

        Task<Events> UserUpdateYourEvent(Guid eventId, Guid userId, Events events);
      

    }
}
