using CompanyEmployeeProject.Companies;
using CompanyEmployeeProject.Employees;
using CompanyEmployeeProject.Projects;
using CompanyEmployeeProject.Tasks;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace CompanyEmployeeProject;

[Mapper]
public partial class CompanyEmployeeProjectApplicationMappers
{
    public static partial CompanyDto MapToCompanyDto(Company source);
    public static partial EmployeeDto MapToEmployeeDto(Employee source);
    public static partial ProjectDto MapToProjectDto(Project source);
    public static partial TaskDto MapToTaskDto(Task source);
}
