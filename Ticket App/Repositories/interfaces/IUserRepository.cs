using Ticket_App.Dto;
using Ticket_App.Model;

namespace Ticket_App.Repositories.interfaces
{
    public interface IUserRepository
    {
        public Task<Guid> RegisterUser(UserDto userDto);
        public Task<bool> FindUserByEmail(string email);
        public Task<Users?> GetUserByEmail(string email);
        Task<List<Tickets>> ListUserTickets(Guid id);
        public Task<Users?> GetUserById(Guid id);

    }
}
