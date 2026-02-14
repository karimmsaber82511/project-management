using CompanyEmployeeProject.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CompanyEmployeeProject.Permissions;

public class CompanyEmployeeProjectPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var group = context.AddGroup(CompanyEmployeeProjectPermissions.GroupName);

        var companies = group.AddPermission(CompanyEmployeeProjectPermissions.Companies.Default, L("Permission:Companies"));
        companies.AddChild(CompanyEmployeeProjectPermissions.Companies.Create, L("Permission:Companies.Create"));
        companies.AddChild(CompanyEmployeeProjectPermissions.Companies.Update, L("Permission:Companies.Update"));
        companies.AddChild(CompanyEmployeeProjectPermissions.Companies.Delete, L("Permission:Companies.Delete"));

        var employees = group.AddPermission(CompanyEmployeeProjectPermissions.Employees.Default, L("Permission:Employees"));
        employees.AddChild(CompanyEmployeeProjectPermissions.Employees.Create, L("Permission:Employees.Create"));
        employees.AddChild(CompanyEmployeeProjectPermissions.Employees.Update, L("Permission:Employees.Update"));
        employees.AddChild(CompanyEmployeeProjectPermissions.Employees.Delete, L("Permission:Employees.Delete"));

        var projects = group.AddPermission(CompanyEmployeeProjectPermissions.Projects.Default, L("Permission:Projects"));
        projects.AddChild(CompanyEmployeeProjectPermissions.Projects.Create, L("Permission:Projects.Create"));
        projects.AddChild(CompanyEmployeeProjectPermissions.Projects.Update, L("Permission:Projects.Update"));
        projects.AddChild(CompanyEmployeeProjectPermissions.Projects.Delete, L("Permission:Projects.Delete"));

        var tasks = group.AddPermission(CompanyEmployeeProjectPermissions.Tasks.Default, L("Permission:Tasks"));
        tasks.AddChild(CompanyEmployeeProjectPermissions.Tasks.Create, L("Permission:Tasks.Create"));
        tasks.AddChild(CompanyEmployeeProjectPermissions.Tasks.Update, L("Permission:Tasks.Update"));
        tasks.AddChild(CompanyEmployeeProjectPermissions.Tasks.Delete, L("Permission:Tasks.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CompanyEmployeeProjectResource>(name);
    }
}
