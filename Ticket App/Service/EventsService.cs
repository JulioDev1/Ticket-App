﻿using System.Security.Claims;
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

        public async Task<Users?> GetUserId(Guid userId)
        {

        var user  = await eventRepositories.GetUserById(userId);
        
        return user;
        }

        public async Task<UpdateEventDto> UserUpdateYourEvent(Guid eventId, Guid userId, UpdateEventDto eventsDto)
        {
            var events = await eventRepositories.UserEventCreatorFind(eventId, userId);

            if (events is null)
            {
                throw new Exception("events not found");
            }

            var UpdatedEvent = new UpdateEventDto
            {
                DataInit = events.DateInit,
                Description = events.Description,
                Name=events.Name,
            };

            await eventRepositories.UserEventCreatorUpdate(UpdatedEvent);
            return UpdatedEvent;
        }
    }
}
