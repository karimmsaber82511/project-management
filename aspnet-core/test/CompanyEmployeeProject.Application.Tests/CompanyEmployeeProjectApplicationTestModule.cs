using Volo.Abp.Modularity;

namespace CompanyEmployeeProject;

[DependsOn(
    typeof(CompanyEmployeeProjectApplicationModule),
    typeof(CompanyEmployeeProjectDomainTestModule)
)]
public class CompanyEmployeeProjectApplicationTestModule : AbpModule
{

}
