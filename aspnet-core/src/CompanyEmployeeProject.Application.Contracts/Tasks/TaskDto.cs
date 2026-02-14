using System;
using Volo.Abp.Application.Dtos;

namespace CompanyEmployeeProject.Tasks
{
    public class TaskDto : AuditedEntityDto<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime? DueDate { get; set; }

        public Guid ProjectId { get; set; }
        public string? ProjectName { get; set; }

        public Guid? AssignedToId { get; set; }
        public string? AssignedToName { get; set; }
    }
}
