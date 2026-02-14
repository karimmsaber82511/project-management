using Volo.Abp.Modularity;

namespace CompanyEmployeeProject;

public abstract class CompanyEmployeeProjectApplicationTestBase<TStartupModule> : CompanyEmployeeProjectTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
