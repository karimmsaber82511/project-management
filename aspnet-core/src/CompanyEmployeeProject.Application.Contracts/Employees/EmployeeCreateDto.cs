using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyEmployeeProject.Employees
{
    public class EmployeeCreateDto
    {
        [Required]
        [StringLength(128)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(128)]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(256)]
        public string? Email { get; set; }

        public Guid CompanyId { get; set; }
    }
}
