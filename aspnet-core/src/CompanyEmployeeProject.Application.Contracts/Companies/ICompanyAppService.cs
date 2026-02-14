using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CompanyEmployeeProject.Companies
{
    public interface ICompanyAppService : IApplicationService
    {
        Task<CompanyDto> GetAsync(Guid id);
        Task<PagedResultDto<CompanyDto>> GetListAsync(CompanyGetListInput input);
        Task<CompanyDto> CreateAsync(CompanyCreateDto input);
        Task<CompanyDto> UpdateAsync(Guid id, CompanyUpdateDto input);
        Task DeleteAsync(Guid id);
    }
}
