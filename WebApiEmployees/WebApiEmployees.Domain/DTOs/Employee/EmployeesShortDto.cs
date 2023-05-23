using System.ComponentModel.DataAnnotations;

namespace WebApiEmployees.Domain.DTOs.Employee
{
    public class EmployeesShortDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FullName { get; set; } = string.Empty;
    }
}
