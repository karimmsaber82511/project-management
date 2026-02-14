using System;
using CompanyEmployeeProject.Employees;
using Volo.Abp.Domain.Entities;

namespace CompanyEmployeeProject.Projects
{
    public class EmployeeProject : Entity<Guid>
    {
        public Guid EmployeeId { get; set; }
        public Guid ProjectId { get; set; }

        public Employee Employee { get; set; } = null!;
        public Project Project { get; set; } = null!;

        protected EmployeeProject() { }

        public EmployeeProject(Guid id, Guid employeeId, Guid projectId) : base(id)
        {
            EmployeeId = employeeId;
            ProjectId = projectId;
        }
    }
}
