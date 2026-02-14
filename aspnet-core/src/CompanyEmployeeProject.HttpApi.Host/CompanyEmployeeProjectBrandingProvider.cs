using Microsoft.Extensions.Localization;
using CompanyEmployeeProject.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace CompanyEmployeeProject;

[Dependency(ReplaceServices = true)]
public class CompanyEmployeeProjectBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<CompanyEmployeeProjectResource> _localizer;

    public CompanyEmployeeProjectBrandingProvider(IStringLocalizer<CompanyEmployeeProjectResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
