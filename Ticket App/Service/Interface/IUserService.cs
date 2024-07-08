using Ticket_App.Dto;

namespace Ticket_App.Service.Interface
{
    public interface IUserService
    {
        Task<Guid> RegisterUser(UserDto userDto);
    }
}
