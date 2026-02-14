using System;
using System.Collections.Generic;
using CompanyEmployeeProject.Companies;
using CompanyEmployeeProject.Projects;
using Volo.Abp.Domain.Entities.Auditing;

namespace CompanyEmployeeProject.Employees
{
    public class Employee : AuditedEntity<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public Guid CompanyId { get; set; }

        public Company Company { get; set; } = null!;
        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();

        protected Employee() { }

        public Employee(Guid id, string firstName, string lastName, string? email, Guid companyId) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CompanyId = companyId;
        }
    }
}
