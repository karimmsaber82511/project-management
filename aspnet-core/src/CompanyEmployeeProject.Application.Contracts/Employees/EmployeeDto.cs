using System;
using Volo.Abp.Application.Dtos;

namespace CompanyEmployeeProject.Employees
{
    public class EmployeeDto : AuditedEntityDto<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public Guid CompanyId { get; set; }
        public string? CompanyName { get; set; }
    }
}
