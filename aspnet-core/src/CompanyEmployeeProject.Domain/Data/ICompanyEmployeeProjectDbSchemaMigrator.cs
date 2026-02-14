using System.Threading.Tasks;

namespace CompanyEmployeeProject.Data;

public interface ICompanyEmployeeProjectDbSchemaMigrator
{
    Task MigrateAsync();
}
