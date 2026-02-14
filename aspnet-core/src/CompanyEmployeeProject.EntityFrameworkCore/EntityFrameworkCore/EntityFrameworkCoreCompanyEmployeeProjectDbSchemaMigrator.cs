using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CompanyEmployeeProject.Data;
using Volo.Abp.DependencyInjection;

namespace CompanyEmployeeProject.EntityFrameworkCore;

public class EntityFrameworkCoreCompanyEmployeeProjectDbSchemaMigrator
    : ICompanyEmployeeProjectDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreCompanyEmployeeProjectDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the CompanyEmployeeProjectDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<CompanyEmployeeProjectDbContext>()
            .Database
            .MigrateAsync();
    }
}
