using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CompanyEmployeeProject.Data;

/* This is used if database provider does't define
 * ICompanyEmployeeProjectDbSchemaMigrator implementation.
 */
public class NullCompanyEmployeeProjectDbSchemaMigrator : ICompanyEmployeeProjectDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
