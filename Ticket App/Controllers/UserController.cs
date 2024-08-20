using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql.Internal;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks.Dataflow;
using Ticket_App.Dto;
using Ticket_App.Model;
using Ticket_App.Service.Interface;

namespace Ticket_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
           userService = _userService;
        }

        [HttpPost("register-user")]
        public async Task<ActionResult<Guid>> RegisterUser(UserDto userDto)
        {
            var guid = await userService.RegisterUser(userDto);
            return Ok(guid);

        }
        [HttpGet("get-all-user-tickets")]
        [Authorize]
        public async Task<ActionResult<List<Tickets>>> GetAllUserTickets()
        {

            var id = (User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var guid = Guid.Parse(id);


            var ticket = await userService.ListUserTickets(guid);
            if (ticket.Count > 0)
            {
                new Exception("user not found");
            }

            return ticket;

        }
        [HttpGet("calculate-total")]
        [Authorize]
        public  async Task <ActionResult<decimal>> Calculate()
        {
            var id = (User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var guid = Guid.Parse(id);

            var ticket = await userService.ListUserTickets(guid);

            if(ticket is null)
            {
                new Exception("user not found ");
            }
            var price = ticket!.Sum(p => p.Price);

            var priceTotal = (decimal)price;

            return priceTotal;
        }

    }
}
