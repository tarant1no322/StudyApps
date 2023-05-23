using System.ComponentModel.DataAnnotations;
using WebApiEmployees.Domain.Enums;

namespace WebApiEmployees.Domain.DTOs.User
{
    public class ChangeRoleDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public Role Role { get; set; }
    }
}
