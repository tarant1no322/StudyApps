using System.ComponentModel.DataAnnotations;

namespace WebApiEmployees.Domain.DTOs.Employee
{
    public class EmployeeDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
