using Microsoft.AspNetCore.Mvc;
using Ticket_App.Dto;
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

    }
}
