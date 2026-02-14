using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CompanyEmployeeProject.Employees
{
    public interface IEmployeeAppService : IApplicationService
    {
        Task<EmployeeDto> GetAsync(Guid id);
        Task<PagedResultDto<EmployeeDto>> GetListAsync(EmployeeGetListInput input);
        Task<EmployeeDto> CreateAsync(EmployeeCreateDto input);
        Task<EmployeeDto> UpdateAsync(Guid id, EmployeeUpdateDto input);
        Task DeleteAsync(Guid id);
    }
}
