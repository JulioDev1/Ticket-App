using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Ticket_App.Controllers.Dto;
using Ticket_App.Dto;
using Ticket_App.Model;
using Ticket_App.Repositories.interfaces;
using Ticket_App.Service.Interface;

namespace Ticket_App.Service
{
    public class EventsService : IEventsService
    {
        private readonly IEventRepositories eventRepositories;
        

        public EventsService(IEventRepositories _eventRepositories)
        {
            eventRepositories = _eventRepositories;
        }
        public async Task<Guid> CreateEvent(EventsDto eventsDto, TicketsDto ticketsDto)
        {


            var eventCreate = new EventsDto
            {
                Description = eventsDto.Description,
                Name = eventsDto.Name,
                UserId = eventsDto.UserId,  
                DataInit = eventsDto.DataInit,  
            };

            var createEvent = await eventRepositories.CreateEvent(eventCreate);

            var guid = await eventRepositories.CreateTicketAtEvent(ticketsDto, createEvent);

            return guid;
        }

        public Task<bool> DeleteUserEvent(Guid eventId, Guid userId)
        {
            var eventDelete = eventRepositories.DeleteUserEvent(eventId, userId);

            return eventDelete;
        }

        public async Task<List<Events>> GetAllEventsCreatedByUser(Guid Id)
        {
            return await eventRepositories.GetAllEventsCreatedByUser(Id);
        }

        public async Task<Events?> GetEventById(Guid eventId, Guid userId)
        {
            var eventsUser = await eventRepositories.UserEventCreatorFind(eventId,userId);
            
            if (eventsUser == null) {
                throw new Exception("event not found");
            }

            return eventsUser;
        }

        public async Task<Users?> GetUserId(Guid userId)
        {

        var user  = await eventRepositories.GetUserById(userId);
        
        return user;
        }

        public async Task<List<Events>> ListAllTickets()
        {
            return await eventRepositories.ListAllTickets();
        }

        public async Task<Events> UserUpdateYourEvent( Events events)
        {
            var eventsUser = await eventRepositories.UserEventCreatorFind(events.Id, events.UserId);

            if (eventsUser is null)
            {
                throw new Exception("events not found");
            }

            eventsUser.Description = events.Description;
            eventsUser.Name = events.Name;
            eventsUser.DateInit = events.DateInit;  
                

            await eventRepositories.UserEventCreatorUpdate(eventsUser);

            return eventsUser;
        }

    }
}
