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

namespace CompanyEmployeeProject.Employees
{
    [Authorize(CompanyEmployeeProjectPermissions.Employees.Default)]
    public class EmployeeAppService : ApplicationService, IEmployeeAppService
    {
        private readonly IRepository<Employee, Guid> _repository;
        private readonly IRepository<Companies.Company, Guid> _companyRepository;

        public EmployeeAppService(
            IRepository<Employee, Guid> repository,
            IRepository<Companies.Company, Guid> companyRepository)
        {
            _repository = repository;
            _companyRepository = companyRepository;
        }

        public async Task<EmployeeDto> GetAsync(Guid id)
        {
            var query = await _repository.GetQueryableAsync();
            query = query.Include(e => e.Company);
            var entity = await query.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (entity == null) throw new Volo.Abp.Domain.Entities.EntityNotFoundException(typeof(Employee), id);
            var dto = ObjectMapper.Map<Employee, EmployeeDto>(entity);
            if (entity.Company != null) dto.CompanyName = entity.Company.Name;
            return dto;
        }

        public async Task<PagedResultDto<EmployeeDto>> GetListAsync(EmployeeGetListInput input)
        {
            var query = await _repository.GetQueryableAsync();
            query = query.Include(e => e.Company);
            query = query.WhereIf(input.CompanyId.HasValue, e => e.CompanyId == input.CompanyId);
            query = query.WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.FirstName.Contains(input.Filter!) || e.LastName.Contains(input.Filter!) || (e.Email != null && e.Email.Contains(input.Filter!))
            );
            var totalCount = await AsyncExecuter.CountAsync(query);
            query = ApplySorting(query, input);
            query = query.Skip(input.SkipCount).Take(input.MaxResultCount);
            var items = await AsyncExecuter.ToListAsync(query);
            var dtos = items.Select(entity => ObjectMapper.Map<Employee, EmployeeDto>(entity)).ToList();
            foreach (var d in dtos)
            {
                var emp = items.First(e => e.Id == d.Id);
                if (emp.Company != null) d.CompanyName = emp.Company.Name;
            }
            return new PagedResultDto<EmployeeDto>(totalCount, dtos);
        }

        private IQueryable<Employee> ApplySorting(IQueryable<Employee> query, EmployeeGetListInput input)
        {
            return input.Sorting?.ToLower() switch
            {
                "firstname desc" => query.OrderByDescending(e => e.FirstName),
                "firstname" => query.OrderBy(e => e.FirstName),
                "lastname desc" => query.OrderByDescending(e => e.LastName),
                "lastname" => query.OrderBy(e => e.LastName),
                _ => query.OrderBy(e => e.LastName)
            };
        }

        [Authorize(CompanyEmployeeProjectPermissions.Employees.Create)]
        public async Task<EmployeeDto> CreateAsync(EmployeeCreateDto input)
        {
            var entity = new Employee(
                GuidGenerator.Create(),
                input.FirstName,
                input.LastName,
                input.Email,
                input.CompanyId);
            await _repository.InsertAsync(entity);
            var company = await _companyRepository.GetAsync(input.CompanyId);
            entity.Company = company;
            var dto = ObjectMapper.Map<Employee, EmployeeDto>(entity);
            dto.CompanyName = company.Name;
            return dto;
        }

        [Authorize(CompanyEmployeeProjectPermissions.Employees.Update)]
        public async Task<EmployeeDto> UpdateAsync(Guid id, EmployeeUpdateDto input)
        {
            var query = await _repository.GetQueryableAsync();
            query = query.Include(e => e.Company);
            var entity = await query.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (entity == null) throw new Volo.Abp.Domain.Entities.EntityNotFoundException(typeof(Employee), id);
            entity.FirstName = input.FirstName;
            entity.LastName = input.LastName;
            entity.Email = input.Email;
            entity.CompanyId = input.CompanyId;
            await _repository.UpdateAsync(entity);
            var dto = ObjectMapper.Map<Employee, EmployeeDto>(entity);
            var company = await _companyRepository.GetAsync(input.CompanyId);
            dto.CompanyName = company.Name;
            return dto;
        }

        [Authorize(CompanyEmployeeProjectPermissions.Employees.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
