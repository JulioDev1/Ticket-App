using Ticket_App.Dto;

namespace Ticket_App.Service.Interface
{
    public interface ITokenService
    {
       Task<string> GenerateToken(LoginDto loginDto);
    }
}
