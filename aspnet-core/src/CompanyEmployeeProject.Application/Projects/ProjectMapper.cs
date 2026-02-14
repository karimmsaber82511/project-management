using CompanyEmployeeProject;
using Volo.Abp.Mapperly;

namespace CompanyEmployeeProject.Projects
{
    public class ProjectMapper : MapperBase<Project, ProjectDto>
    {
        public override ProjectDto Map(Project source)
        {
            var destination = new ProjectDto();
            Map(source, destination);
            return destination;
        }

        public override void Map(Project source, ProjectDto destination)
        {
            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.StartDate = source.StartDate;
            destination.EndDate = source.EndDate;
            destination.CompanyId = source.CompanyId;
            destination.CompanyName = source.Company?.Name;
            destination.CreationTime = source.CreationTime;
            destination.CreatorId = source.CreatorId;
            destination.LastModificationTime = source.LastModificationTime;
            destination.LastModifierId = source.LastModifierId;
        }
    }
}
