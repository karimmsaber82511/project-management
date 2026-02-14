using System;
using Volo.Abp.Application.Dtos;

namespace CompanyEmployeeProject.Companies
{
    public class CompanyDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
    }
}
