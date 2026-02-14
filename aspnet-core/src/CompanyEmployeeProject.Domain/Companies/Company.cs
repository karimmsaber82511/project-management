using System;
using System.Collections.Generic;
using CompanyEmployeeProject.Employees;
using CompanyEmployeeProject.Projects;
using Volo.Abp.Domain.Entities.Auditing;

namespace CompanyEmployeeProject.Companies
{
    public class Company : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();

        protected Company() { }

        public Company(Guid id, string name, string? address = null) : base(id)
        {
            Name = name;
            Address = address;
        }
    }
}
