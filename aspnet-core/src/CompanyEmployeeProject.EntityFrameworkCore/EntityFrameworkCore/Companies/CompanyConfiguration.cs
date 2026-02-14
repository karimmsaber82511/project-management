using CompanyEmployeeProject.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyEmployeeProject.EntityFrameworkCore.Companies
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");
            builder.Property(c => c.Name).IsRequired().HasMaxLength(256);
            builder.Property(c => c.Address).HasMaxLength(512);
        }
    }
}
