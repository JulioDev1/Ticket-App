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
        public async Task <ActionResult<Guid>> userBuyedTicketEvent(Guid userId, Guid TicketId, PaymentDto paymentDto)
        {
            try
            {
                var id = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var email = (User.Claims.First(c => c.Type == ClaimTypes.Name));



                if (id == Guid.Empty && email == null)
                {
                    Unauthorized("user desconnected");
                }


                var ticket = await ticketsService.FindTicketById(TicketId);

                if (ticket is null)
                {
                    throw new Exception("ticket is not exists");
                }



                var price = (decimal)ticket.Price;

                paymentDto.TransactionAmount = price;
                paymentDto.Email = email!.Value;

                
                var guid = await ticketsService.UserBuyedTicketEvent(id, TicketId);

                await ticketsService.CreatePayment(paymentDto);

                return Ok();
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);  
            }   
            
        }
    }
}
