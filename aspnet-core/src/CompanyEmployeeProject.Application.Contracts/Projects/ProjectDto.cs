using System;
using Volo.Abp.Application.Dtos;

namespace CompanyEmployeeProject.Projects
{
    public class ProjectDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid CompanyId { get; set; }
        public string? CompanyName { get; set; }
    }
}
