using System;
using System.Collections.Generic;
using CompanyEmployeeProject.Companies;
using Volo.Abp.Domain.Entities.Auditing;

namespace CompanyEmployeeProject.Projects
{
    public class Project : AuditedEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid CompanyId { get; set; }

        public Company Company { get; set; } = null!;
        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();

        protected Project() { }

        public Project(Guid id, string name, DateTime? startDate, DateTime? endDate, Guid companyId) : base(id)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            CompanyId = companyId;
        }
    }
}
