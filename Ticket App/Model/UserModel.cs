
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Ticket_App.Model
{
    [Index(nameof(Email), IsUnique = true)]

    public class Users
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Type { get; set; }
        public List<UserTickets> userTickets { get; } = [];
        public List<Tickets> Tickets { get; }= [];
        public ICollection<Events> Event { get; }= new List<Events>();

    }

    public class Events
    {
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime Created { get; set; }   
        public Guid UserId { get; set; }
        public Users? User { get; set; }
        public Tickets Ticket { get; set; } = null!; 
    }
    
    public class Tickets
    {
        [Key]
        public Guid Id { get; set; }    
        public required string Name { get; set; }
        public float Price { get; set;}
        public Guid EventId { get; set; }
        public required Events  Event  { get; set; }
        public List<UserTickets> userTickets { get; } = [];
        public List<Users> Users { get; } = [];
    }

    public class UserTickets
    {
        [Key]
        public Guid UsersId { get; set; }    
        public Guid TicketId { get; set; }
        public Users User { get; set; } = null!;
        public Tickets Ticket { get; set;} = null!;
    }
}
