using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Ticket_App.Dto;
using Ticket_App.Service.Interface;

namespace Ticket_App.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    
    {
        private readonly ITicketsService ticketsService;
        public TicketController (ITicketsService ticketsService)
        {
            this.ticketsService = ticketsService;
        }    

        [HttpPost("ticket-buy")]
        [Authorize]


        public async Task <ActionResult<Guid>> userBuyedTicketEvent(Guid userId, Guid TicketId)
        {
            try
            {
                var id = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

                if (id == Guid.Empty)
                {
                    Unauthorized("user desconnected");
                }


                var ticket = await ticketsService.FindTicketById(TicketId);

                if (ticket is null)
                {
                    throw new Exception("ticket is not exists");
                }

                var price = (decimal)ticket.Price;
                
                var guid = await ticketsService.UserBuyedTicketEvent(id, TicketId);


                return Ok();
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);  
            }   
            
        }
    }
}
