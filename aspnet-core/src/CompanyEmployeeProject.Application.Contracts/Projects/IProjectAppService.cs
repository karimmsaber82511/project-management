using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CompanyEmployeeProject.Projects
{
    public interface IProjectAppService : IApplicationService
    {
        Task<ProjectDto> GetAsync(Guid id);
        Task<PagedResultDto<ProjectDto>> GetListAsync(ProjectGetListInput input);
        Task<ProjectDto> CreateAsync(ProjectCreateDto input);
        Task<ProjectDto> UpdateAsync(Guid id, ProjectUpdateDto input);
        Task DeleteAsync(Guid id);
    }
}
