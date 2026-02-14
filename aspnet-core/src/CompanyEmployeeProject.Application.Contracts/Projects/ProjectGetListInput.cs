using System;
using Volo.Abp.Application.Dtos;

namespace CompanyEmployeeProject.Projects
{
    public class ProjectGetListInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
