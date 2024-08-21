﻿using Microsoft.EntityFrameworkCore;
using Ticket_App.Context;
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


        public async Task<Users?> GetUserById(Guid Id)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Id == Id);
        }

        public async Task<Events?> UserEventCreatorFind(Guid eventId, Guid userId)
        {
            return await context.Events.FirstOrDefaultAsync(events => events.Id == eventId && events.UserId == userId);
        }

        public async Task<Events> UserEventCreatorUpdate(EventsDto eventsDto)
        {
            var updatedEvent = new Events
            {
                DateInit = eventsDto.DataInit,
                Description = eventsDto.Description,
                Name = eventsDto.Name,

            };
            context.Events.Update(updatedEvent);
            await context.SaveChangesAsync();
            return updatedEvent;
        }
    }
}
