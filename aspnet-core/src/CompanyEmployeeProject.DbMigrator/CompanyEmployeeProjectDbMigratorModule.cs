using CompanyEmployeeProject.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace CompanyEmployeeProject.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CompanyEmployeeProjectEntityFrameworkCoreModule),
    typeof(CompanyEmployeeProjectApplicationContractsModule)
    )]
public class CompanyEmployeeProjectDbMigratorModule : AbpModule
{
}
