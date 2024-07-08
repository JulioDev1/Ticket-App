using Ticket_App.Dto;

namespace Ticket_App.Repositories.interfaces
{
    public interface IUserRepository
    {
        public Task<Guid> RegisterUser(UserDto userDto);
    }
}
