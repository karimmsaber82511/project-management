using CompanyEmployeeProject.Samples;
using Xunit;

namespace CompanyEmployeeProject.EntityFrameworkCore.Domains;

[Collection(CompanyEmployeeProjectTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<CompanyEmployeeProjectEntityFrameworkCoreTestModule>
{

}
