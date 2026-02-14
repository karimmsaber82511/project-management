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

namespace CompanyEmployeeProject.Projects
{
    [Authorize(CompanyEmployeeProjectPermissions.Projects.Default)]
    public class ProjectAppService : ApplicationService, IProjectAppService
    {
        private readonly IRepository<Project, Guid> _repository;
        private readonly IRepository<Companies.Company, Guid> _companyRepository;

        public ProjectAppService(
            IRepository<Project, Guid> repository,
            IRepository<Companies.Company, Guid> companyRepository)
        {
            _repository = repository;
            _companyRepository = companyRepository;
        }

        public async Task<ProjectDto> GetAsync(Guid id)
        {
            var query = await _repository.GetQueryableAsync();
            query = query.Include(p => p.Company);
            var entity = await query.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (entity == null) throw new Volo.Abp.Domain.Entities.EntityNotFoundException(typeof(Project), id);
            var dto = ObjectMapper.Map<Project, ProjectDto>(entity);
            if (entity.Company != null) dto.CompanyName = entity.Company.Name;
            return dto;
        }

        public async Task<PagedResultDto<ProjectDto>> GetListAsync(ProjectGetListInput input)
        {
            var query = await _repository.GetQueryableAsync();
            query = query.Include(p => p.Company);
            query = query.WhereIf(input.CompanyId.HasValue, p => p.CompanyId == input.CompanyId);
            query = query.WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                p => p.Name.Contains(input.Filter!)
            );
            var totalCount = await AsyncExecuter.CountAsync(query);
            query = ApplySorting(query, input);
            query = query.Skip(input.SkipCount).Take(input.MaxResultCount);
            var items = await AsyncExecuter.ToListAsync(query);
            var dtos = items.Select(entity => ObjectMapper.Map<Project, ProjectDto>(entity)).ToList();
            foreach (var d in dtos)
            {
                var proj = items.First(p => p.Id == d.Id);
                if (proj.Company != null) d.CompanyName = proj.Company.Name;
            }
            return new PagedResultDto<ProjectDto>(totalCount, dtos);
        }

        private IQueryable<Project> ApplySorting(IQueryable<Project> query, ProjectGetListInput input)
        {
            return input.Sorting?.ToLower() switch
            {
                "name desc" => query.OrderByDescending(p => p.Name),
                "name" => query.OrderBy(p => p.Name),
                "startdate desc" => query.OrderByDescending(p => p.StartDate),
                "startdate" => query.OrderBy(p => p.StartDate),
                _ => query.OrderBy(p => p.Name)
            };
        }

        [Authorize(CompanyEmployeeProjectPermissions.Projects.Create)]
        public async Task<ProjectDto> CreateAsync(ProjectCreateDto input)
        {
            var entity = new Project(
                GuidGenerator.Create(),
                input.Name,
                input.StartDate,
                input.EndDate,
                input.CompanyId);
            await _repository.InsertAsync(entity);
            var dto = ObjectMapper.Map<Project, ProjectDto>(entity);
            var company = await _companyRepository.GetAsync(input.CompanyId);
            dto.CompanyName = company.Name;
            return dto;
        }

        [Authorize(CompanyEmployeeProjectPermissions.Projects.Update)]
        public async Task<ProjectDto> UpdateAsync(Guid id, ProjectUpdateDto input)
        {
            var query = await _repository.GetQueryableAsync();
            query = query.Include(p => p.Company);
            var entity = await query.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (entity == null) throw new Volo.Abp.Domain.Entities.EntityNotFoundException(typeof(Project), id);
            entity.Name = input.Name;
            entity.StartDate = input.StartDate;
            entity.EndDate = input.EndDate;
            entity.CompanyId = input.CompanyId;
            await _repository.UpdateAsync(entity);
            var dto = ObjectMapper.Map<Project, ProjectDto>(entity);
            var company = await _companyRepository.GetAsync(input.CompanyId);
            dto.CompanyName = company.Name;
            return dto;
        }

        [Authorize(CompanyEmployeeProjectPermissions.Projects.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
