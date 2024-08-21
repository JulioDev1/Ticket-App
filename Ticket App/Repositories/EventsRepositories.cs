using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;
using Ticket_App.Context;
using Ticket_App.Controllers.Dto;
using Ticket_App.Dto;
using Ticket_App.Model;
using Ticket_App.Repositories.interfaces;

namespace Ticket_App.Repositories
{
    public class EventsRepositories : IEventRepositories
    {
        private readonly UserContext context;
        public EventsRepositories( UserContext _context) { 
            context = _context;
        }
        public async Task<Events> CreateEvent(EventsDto eventsDto)
        {
            DateTime formatedData = DateTime.Now;

            var createEvent = new Events
            {
                Name = eventsDto.Name,
                Description = eventsDto.Description,
                UserId = eventsDto.UserId,
                DateInit = eventsDto.DataInit,
                Created = formatedData
            };

            await context.Events.AddAsync(createEvent);
            return createEvent;
        }

        public async Task<Guid> CreateTicketAtEvent(TicketsDto ticketsDto, Events events)
        {
            var tickets = new Tickets
            {
                Name = ticketsDto.Name,
                Price = ticketsDto.Price,
                Event = events
            };
            context.Tickets.Add(tickets);
            await context.SaveChangesAsync();
            return tickets.EventId;  
        }

        public async Task<bool> DeleteUserEvent(Guid eventId, Guid userId)
        {
            var eventExists = await UserEventCreatorFind(eventId,userId);
            
            if (eventExists is null)
            {
                throw new Exception("user event not exists");
            }

            context.Events.Remove(eventExists);
            
            await context.SaveChangesAsync();

            return true;
        }
        public async Task<List<Events>> GetAllEventsCreatedByUser(Guid id)
        {
            return await context.Users.Where(e => e.Id == id).SelectMany(u => u.Event).ToListAsync();
        }

        public async Task<Users?> GetUserById(Guid Id)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Id == Id);
        }

        public async Task<List<Events>> ListAllTickets()
        {
            return await context.Events.ToListAsync();
        }

        public async Task<Events?> UserEventCreatorFind(Guid eventId, Guid userId)
        {
            return await context.Events.FirstOrDefaultAsync(events => events.Id == eventId && events.UserId == userId);
        }

        public async Task<Events> UserEventCreatorUpdate(Events events)
     
        {
            var eventExists = await UserEventCreatorFind(events.Id, events.UserId);
            if(eventExists is null)
            {
                throw new Exception("user event not exists");
            }
            eventExists.DateInit = events.DateInit;
            eventExists.Description = events.Description;
            eventExists.Name = events.Name;

            context.Events.Update(eventExists);
            await context.SaveChangesAsync();
            return eventExists;
        }
    }
}
