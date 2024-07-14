using System.Security.Claims;
using Ticket_App.Dto;

namespace Ticket_App.Service.Interface
{
    public interface ITokenService
    {
       Task<string> GenerateToken(LoginDto loginDto);
        bool VerifyValidToken(string token, out ClaimsPrincipal claims);
    }
}
