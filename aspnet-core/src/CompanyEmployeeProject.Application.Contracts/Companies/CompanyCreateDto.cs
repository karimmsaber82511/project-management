using System.ComponentModel.DataAnnotations;

namespace CompanyEmployeeProject.Companies
{
    public class CompanyCreateDto
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; } = string.Empty;

        [StringLength(512)]
        public string? Address { get; set; }
    }
}
