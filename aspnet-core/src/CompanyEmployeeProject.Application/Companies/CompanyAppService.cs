using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using CompanyEmployeeProject.Permissions;

namespace CompanyEmployeeProject.Companies
{
    [Authorize(CompanyEmployeeProjectPermissions.Companies.Default)]
    public class CompanyAppService : ApplicationService, ICompanyAppService
    {
        private readonly IRepository<Company, Guid> _repository;

        public CompanyAppService(IRepository<Company, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<CompanyDto> GetAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return ObjectMapper.Map<Company, CompanyDto>(entity);
        }

        public async Task<PagedResultDto<CompanyDto>> GetListAsync(CompanyGetListInput input)
        {
            var query = await _repository.GetQueryableAsync();
            query = query.WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                c => c.Name.Contains(input.Filter!) || (c.Address != null && c.Address.Contains(input.Filter!))
            );
            var totalCount = await AsyncExecuter.CountAsync(query);
            query = ApplySorting(query, input);
            query = query.Skip(input.SkipCount).Take(input.MaxResultCount);
            var items = await AsyncExecuter.ToListAsync(query);
            var dtos = items.Select(entity => ObjectMapper.Map<Company, CompanyDto>(entity)).ToList();
            return new PagedResultDto<CompanyDto>(totalCount, dtos);
        }

        private IQueryable<Company> ApplySorting(IQueryable<Company> query, CompanyGetListInput input)
        {
            return input.Sorting?.ToLower() switch
            {
                "name desc" => query.OrderByDescending(c => c.Name),
                "name" => query.OrderBy(c => c.Name),
                "address desc" => query.OrderByDescending(c => c.Address),
                "address" => query.OrderBy(c => c.Address),
                _ => query.OrderBy(c => c.Name)
            };
        }

        [Authorize(CompanyEmployeeProjectPermissions.Companies.Create)]
        public async Task<CompanyDto> CreateAsync(CompanyCreateDto input)
        {
            var entity = new Company(GuidGenerator.Create(), input.Name, input.Address);
            await _repository.InsertAsync(entity);
            return ObjectMapper.Map<Company, CompanyDto>(entity);
        }

        [Authorize(CompanyEmployeeProjectPermissions.Companies.Update)]
        public async Task<CompanyDto> UpdateAsync(Guid id, CompanyUpdateDto input)
        {
            var entity = await _repository.GetAsync(id);
            entity.Name = input.Name;
            entity.Address = input.Address;
            await _repository.UpdateAsync(entity);
            return ObjectMapper.Map<Company, CompanyDto>(entity);
        }

        [Authorize(CompanyEmployeeProjectPermissions.Companies.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
