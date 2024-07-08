
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Ticket_App.Model
{
[Index(nameof(Email), IsUnique =true)]

    public class User
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

    }
}
