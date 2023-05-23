using System.ComponentModel.DataAnnotations;
using WebApiEmployees.Domain.Enums;

namespace WebApiEmployees.Domain.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public Role Role { get; set; }
    }
}
