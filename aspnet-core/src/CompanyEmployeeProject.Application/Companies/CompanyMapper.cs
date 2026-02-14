using CompanyEmployeeProject;
using Volo.Abp.Mapperly;

namespace CompanyEmployeeProject.Companies
{
    public class CompanyMapper : MapperBase<Company, CompanyDto>
    {
        public override CompanyDto Map(Company source)
        {
            return CompanyEmployeeProjectApplicationMappers.MapToCompanyDto(source);
        }

        public override void Map(Company source, CompanyDto destination)
        {
            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.Address = source.Address;
            destination.CreationTime = source.CreationTime;
            destination.CreatorId = source.CreatorId;
            destination.LastModificationTime = source.LastModificationTime;
            destination.LastModifierId = source.LastModifierId;
        }
    }
}
