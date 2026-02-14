using System;
using CompanyEmployeeProject.Employees;
using CompanyEmployeeProject.Projects;
using Volo.Abp.Domain.Entities.Auditing;

namespace CompanyEmployeeProject.Tasks
{
    public class Task : AuditedEntity<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.ToDo;
        public DateTime? DueDate { get; set; }

        public Guid ProjectId { get; set; }
        public Guid? AssignedToId { get; set; }

        public Project Project { get; set; } = null!;
        public Employee? AssignedTo { get; set; }

        protected Task() { }

        public Task(
            Guid id,
            string title,
            string? description,
            TaskStatus status,
            DateTime? dueDate,
            Guid projectId,
            Guid? assignedToId = null) : base(id)
        {
            Title = title;
            Description = description;
            Status = status;
            DueDate = dueDate;
            ProjectId = projectId;
            AssignedToId = assignedToId;
        }
    }
}
