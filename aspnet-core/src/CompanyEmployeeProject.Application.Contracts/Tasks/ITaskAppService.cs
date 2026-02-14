using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CompanyEmployeeProject.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        Task<TaskDto> GetAsync(Guid id);
        Task<PagedResultDto<TaskDto>> GetListAsync(TaskGetListInput input);
        Task<TaskDto> CreateAsync(TaskCreateDto input);
        Task<TaskDto> UpdateAsync(Guid id, TaskUpdateDto input);
        Task DeleteAsync(Guid id);
    }
}
