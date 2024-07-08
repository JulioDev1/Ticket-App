using Ticket_App.Context;
using Ticket_App.Dto;
using Ticket_App.Model;
using Ticket_App.Repositories.interfaces;

namespace Ticket_App.Repositories
{
    public class UserRepostories : IUserRepository
    {
        private readonly UserContext context;
        public UserRepostories(UserContext _context) {
            context = _context;
        }

        public async Task<Guid> RegisterUser(UserDto userDto)
        {
            var user = new User
            {
                Name= userDto.Name,
                Email= userDto.Email,
                Password= userDto.Password,
                Type = userDto.Type                

            };
            
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user.Id;
        }
    }
}
