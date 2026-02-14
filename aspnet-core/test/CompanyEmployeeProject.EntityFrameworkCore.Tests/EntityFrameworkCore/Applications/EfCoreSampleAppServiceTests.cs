using CompanyEmployeeProject.Samples;
using Xunit;

namespace CompanyEmployeeProject.EntityFrameworkCore.Applications;

[Collection(CompanyEmployeeProjectTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<CompanyEmployeeProjectEntityFrameworkCoreTestModule>
{

}
