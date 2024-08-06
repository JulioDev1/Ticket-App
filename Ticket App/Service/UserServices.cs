using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Ticket_App.Dto;
using Ticket_App.Model;
using Ticket_App.Repositories;
using Ticket_App.Repositories.interfaces;
using Ticket_App.Service.Interface;

namespace Ticket_App.Service
{
    public class UserServices : IUserService
    {
        private IUserRepository userRepository;
        private readonly PasswordHasher<string> criptography;

        public UserServices (IUserRepository _userRepository)
        {
            userRepository = _userRepository;
            criptography = new PasswordHasher<string> ();
        }
        
       
       
        public async Task<Guid> RegisterUser(UserDto userDto)
        {
            var emailExists = await userRepository.FindUserByEmail(userDto.Email);
            if (emailExists)
            {
                throw new Exception("email already exists");
            }

            var user = new UserDto
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Type = userDto.Type,
            };
            user.Password = criptography.HashPassword(null, password: userDto.Password);
            var guid =  await userRepository.RegisterUser(user);
        
            return guid;
        }

        public async Task<Users?> GetUserByEmail(string email)
        {
            return await userRepository.GetUserByEmail(email);
        }

        public async Task<Users?> GetUserById(Guid id)
        {
           return await userRepository.GetUserById(id);
        }

        public async Task<List<Tickets>> ListUserTickets(Guid id)
        {
            return await userRepository.ListUserTickets(id);
        }
    }
}
