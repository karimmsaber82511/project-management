using Volo.Abp.Modularity;

namespace CompanyEmployeeProject;

/* Inherit from this class for your domain layer tests. */
public abstract class CompanyEmployeeProjectDomainTestBase<TStartupModule> : CompanyEmployeeProjectTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
