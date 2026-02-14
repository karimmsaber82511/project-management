using CompanyEmployeeProject.Companies;
using CompanyEmployeeProject.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyEmployeeProject.EntityFrameworkCore.Employees
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(128);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(128);
            builder.Property(e => e.Email).HasMaxLength(256);

            builder.HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.CompanyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
