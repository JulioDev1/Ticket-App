using Ticket_App.Dto;
using Ticket_App.Model;

namespace Ticket_App.Service.Interface
{
    public interface IUserService
    {
        Task<Guid> RegisterUser(UserDto userDto);
        Task<Users?> GetUserByEmail(string email);
        Task<Users?> GetUserById(Guid id);
        Task<List<Tickets>> ListUserTickets(Guid id);


    }
}
