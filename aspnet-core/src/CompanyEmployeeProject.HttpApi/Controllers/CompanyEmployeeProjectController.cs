using CompanyEmployeeProject.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CompanyEmployeeProject.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CompanyEmployeeProjectController : AbpControllerBase
{
    protected CompanyEmployeeProjectController()
    {
        LocalizationResource = typeof(CompanyEmployeeProjectResource);
    }
}
