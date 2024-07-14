using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ticket_App.Dto;
using Ticket_App.Service.Interface;

namespace Ticket_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenService tokenService;

        public AuthenticationController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }
        [HttpPost("user-auth")]
        public async Task <ActionResult<string>> Login(LoginDto loginDto)
        {
            var AcessToken = await tokenService.GenerateToken(loginDto);

            if(AcessToken == "")
                return Unauthorized();

            return Ok(AcessToken);
        }
        [HttpGet("verify")]
        public ActionResult verifyToken(string token)

        {
            if(tokenService.VerifyValidToken(token,  out var claims))
            {

                return Ok("token is valid");
            }
            return Unauthorized();
        }
    }
}
