using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Claims;
using Ticket_App.Dto;
using Ticket_App.Dto.NovaPasta2;
using Ticket_App.Model;
using Ticket_App.Service.Interface;


namespace Ticket_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
    private readonly IEventsService eventsService;
    

        public EventsController(IEventsService _eventsService)
        {
            eventsService = _eventsService;  
        }

        [HttpPost("create-event")]

        [Authorize]
        public async Task<ActionResult<Guid>> CreateEvent([FromBody] CreateEventRequest request)
        {
            var userId = Guid.Parse(User.Claims.First(c=> c.Type == ClaimTypes.NameIdentifier).Value);

            if(userId == Guid.Empty)
            {
                Unauthorized("user desconnected");
            }

            request.eventsDto.UserId = userId;

            var user = await eventsService.CreateEvent(request.eventsDto, request.ticketsDto);

            return user;
        }
        [HttpPatch("update-event")]
        public async Task<ActionResult<Events>> UpdateUserEvent([FromQuery] Guid eventId, EventsDto eventsDto)
        {
            var userId = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (userId == Guid.Empty)
            {
                Unauthorized("user desconnected");
            }
            
            var updatedEvent = await eventsService.UserUpdateYourEvent(eventId, userId, eventsDto);
           
            return Ok(updatedEvent);
        }
    }
}
