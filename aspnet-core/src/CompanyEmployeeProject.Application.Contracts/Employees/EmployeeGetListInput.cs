using System;
using Volo.Abp.Application.Dtos;

namespace CompanyEmployeeProject.Employees
{
    public class EmployeeGetListInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
