using System;
using Volo.Abp.Application.Dtos;

namespace CompanyEmployeeProject.Tasks
{
    public class TaskGetListInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? AssignedToId { get; set; }
        public TaskStatus? Status { get; set; }
    }
}
