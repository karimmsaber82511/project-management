using Volo.Abp.Modularity;

namespace CompanyEmployeeProject;

[DependsOn(
    typeof(CompanyEmployeeProjectDomainModule),
    typeof(CompanyEmployeeProjectTestBaseModule)
)]
public class CompanyEmployeeProjectDomainTestModule : AbpModule
{

}
