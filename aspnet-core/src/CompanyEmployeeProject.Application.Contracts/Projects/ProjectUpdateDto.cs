using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyEmployeeProject.Projects
{
    public class ProjectUpdateDto
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; } = string.Empty;

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid CompanyId { get; set; }
    }
}
