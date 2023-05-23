using System.ComponentModel.DataAnnotations;

namespace WebApiEmployees.Domain.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public DateTime DateCreate { get; set; }

        [Required]
        public DateTime DateUpdate { get; set; }

        public string? Description { get; set; }

    }
}
