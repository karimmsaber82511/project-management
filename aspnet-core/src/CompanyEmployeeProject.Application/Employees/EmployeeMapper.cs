using CompanyEmployeeProject;
using Volo.Abp.Mapperly;

namespace CompanyEmployeeProject.Employees
{
    public class EmployeeMapper : MapperBase<Employee, EmployeeDto>
    {
        public override EmployeeDto Map(Employee source)
        {
            return CompanyEmployeeProjectApplicationMappers.MapToEmployeeDto(source);
        }

        public override void Map(Employee source, EmployeeDto destination)
        {
            destination.Id = source.Id;
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.Email = source.Email;
            destination.CompanyId = source.CompanyId;
            destination.CompanyName = source.Company?.Name;
            destination.CreationTime = source.CreationTime;
            destination.CreatorId = source.CreatorId;
            destination.LastModificationTime = source.LastModificationTime;
            destination.LastModifierId = source.LastModifierId;
        }
    }
}
