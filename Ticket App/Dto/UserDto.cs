﻿
namespace Ticket_App.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }   
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Type {  get; set; }

    }
}
