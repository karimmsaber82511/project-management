using CompanyEmployeeProject;
using Volo.Abp.Mapperly;
using TaskEntity = CompanyEmployeeProject.Tasks.Task;

namespace CompanyEmployeeProject.Tasks
{
    public class TaskMapper : MapperBase<TaskEntity, TaskDto>
    {
        public override TaskDto Map(TaskEntity source)
        {
            return CompanyEmployeeProjectApplicationMappers.MapToTaskDto(source);
        }

        public override void Map(TaskEntity source, TaskDto destination)
        {
            destination.Id = source.Id;
            destination.Title = source.Title;
            destination.Description = source.Description;
            destination.Status = source.Status;
            destination.DueDate = source.DueDate;
            destination.ProjectId = source.ProjectId;
            destination.AssignedToId = source.AssignedToId;
            destination.CreationTime = source.CreationTime;
            destination.CreatorId = source.CreatorId;
            destination.LastModificationTime = source.LastModificationTime;
            destination.LastModifierId = source.LastModifierId;
        }
    }
}
