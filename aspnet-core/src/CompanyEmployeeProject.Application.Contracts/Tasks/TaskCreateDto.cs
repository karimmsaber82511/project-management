using System;
using System.ComponentModel.DataAnnotations;
using CompanyEmployeeProject.Tasks;

namespace CompanyEmployeeProject.Tasks
{
    public class TaskCreateDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        public TaskStatus Status { get; set; } = TaskStatus.ToDo;

        public DateTime? DueDate { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        public Guid? AssignedToId { get; set; }
    }
}
