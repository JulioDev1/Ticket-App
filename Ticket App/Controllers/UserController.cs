﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("get-all-tickets")]
        [Authorize]
        public async Task<ActionResult<Tickets[]>> GetAllUserTickets(UserDto userDto)
        {
            var email = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.Email).Value);
           

            var user = await userService.GetUserByEmail(email.ToString());
            if (user is null)
            {
                new Exception("user not found ");
            }
            var ticket = user!.Tickets.ToList();
            if(ticket.Count > 0)
            {
                new Exception("user not ticket");
            }
                
            return Ok(ticket);  

        }
        [HttpGet("calculate-total")]
        [Authorize]
        public  async Task <ActionResult<decimal>> Calculate()
        {
            var email = (User.Claims.First(c => c.Type == ClaimTypes.Email).Value);

            var user = await userService.GetUserByEmail(email.ToString());

            if(user is null)
            {
                new Exception("user not found ");
            }
            var price = user!.Tickets.Select(t => t.Price).ToList();

            if(price.Count > 0)
            {
                throw new Exception("no have tickets");
            }

            var priceTotal = (decimal)price.Sum();

            return priceTotal;
        }

    }
}
