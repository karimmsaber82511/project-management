using System;
using System.Collections.Generic;
using System.Text;
using CompanyEmployeeProject.Localization;
using Volo.Abp.Application.Services;

namespace CompanyEmployeeProject;

/* Inherit your application services from this class.
 */
public abstract class CompanyEmployeeProjectAppService : ApplicationService
{
    protected CompanyEmployeeProjectAppService()
    {
        LocalizationResource = typeof(CompanyEmployeeProjectResource);
    }
}
