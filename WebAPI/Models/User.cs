using System.ComponentModel.DataAnnotations;
using WebAPI.Utilities;

namespace WebAPI.Models
{
    public class User
    {
        [Key]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; } = null!;

       public Roles Role { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
        public virtual Address? Address { get; set; }
    }
}
