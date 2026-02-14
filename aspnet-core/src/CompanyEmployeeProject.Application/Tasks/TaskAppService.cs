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
using TaskEntity = CompanyEmployeeProject.Tasks.Task;

namespace CompanyEmployeeProject.Tasks
{
    [Authorize(CompanyEmployeeProjectPermissions.Tasks.Default)]
    public class TaskAppService : ApplicationService, ITaskAppService
    {
        private readonly IRepository<TaskEntity, Guid> _repository;
        private readonly IRepository<Projects.Project, Guid> _projectRepository;
        private readonly IRepository<Employees.Employee, Guid> _employeeRepository;

        public TaskAppService(
            IRepository<TaskEntity, Guid> repository,
            IRepository<Projects.Project, Guid> projectRepository,
            IRepository<Employees.Employee, Guid> employeeRepository)
        {
            _repository = repository;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
        }

        public async System.Threading.Tasks.Task<TaskDto> GetAsync(Guid id)
        {
            var query = await _repository.GetQueryableAsync();
            query = query.Include(t => t.Project);
            query = query.Include(t => t.AssignedTo);
            var entity = await query.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (entity == null) throw new Volo.Abp.Domain.Entities.EntityNotFoundException(typeof(TaskEntity), id);
            var dto = ObjectMapper.Map<TaskEntity, TaskDto>(entity);
            if (entity.Project != null) dto.ProjectName = entity.Project.Name;
            if (entity.AssignedTo != null) dto.AssignedToName = $"{entity.AssignedTo.FirstName} {entity.AssignedTo.LastName}";
            return dto;
        }

        public async System.Threading.Tasks.Task<PagedResultDto<TaskDto>> GetListAsync(TaskGetListInput input)
        {
            var query = await _repository.GetQueryableAsync();
            query = query.Include(t => t.Project);
            query = query.Include(t => t.AssignedTo);
            query = query.WhereIf(input.ProjectId.HasValue, t => t.ProjectId == input.ProjectId);
            query = query.WhereIf(input.AssignedToId.HasValue, t => t.AssignedToId == input.AssignedToId);
            query = query.WhereIf(input.Status.HasValue, t => t.Status == input.Status);
            query = query.WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                t => t.Title.Contains(input.Filter!)
            );
            var totalCount = await AsyncExecuter.CountAsync(query);
            query = ApplySorting(query, input);
            query = query.Skip(input.SkipCount).Take(input.MaxResultCount);
            var items = await AsyncExecuter.ToListAsync(query);
            var dtos = items.Select(entity => ObjectMapper.Map<TaskEntity, TaskDto>(entity)).ToList();
            foreach (var d in dtos)
            {
                var task = items.First(t => t.Id == d.Id);
                if (task.Project != null) d.ProjectName = task.Project.Name;
                if (task.AssignedTo != null) d.AssignedToName = $"{task.AssignedTo.FirstName} {task.AssignedTo.LastName}";
            }
            return new PagedResultDto<TaskDto>(totalCount, dtos);
        }

        private IQueryable<TaskEntity> ApplySorting(IQueryable<TaskEntity> query, TaskGetListInput input)
        {
            return input.Sorting?.ToLower() switch
            {
                "title desc" => query.OrderByDescending(t => t.Title),
                "title" => query.OrderBy(t => t.Title),
                "status desc" => query.OrderByDescending(t => t.Status),
                "status" => query.OrderBy(t => t.Status),
                "duedate desc" => query.OrderByDescending(t => t.DueDate),
                "duedate" => query.OrderBy(t => t.DueDate),
                _ => query.OrderBy(t => t.CreationTime)
            };
        }

        [Authorize(CompanyEmployeeProjectPermissions.Tasks.Create)]
        public async System.Threading.Tasks.Task<TaskDto> CreateAsync(TaskCreateDto input)
        {
            var entity = new TaskEntity(
                GuidGenerator.Create(),
                input.Title,
                input.Description,
                input.Status,
                input.DueDate,
                input.ProjectId,
                input.AssignedToId);
            await _repository.InsertAsync(entity);
            var dto = ObjectMapper.Map<TaskEntity, TaskDto>(entity);
            var project = await _projectRepository.GetAsync(input.ProjectId);
            dto.ProjectName = project.Name;
            if (input.AssignedToId.HasValue)
            {
                var employee = await _employeeRepository.GetAsync(input.AssignedToId.Value);
                dto.AssignedToName = $"{employee.FirstName} {employee.LastName}";
            }
            return dto;
        }

        [Authorize(CompanyEmployeeProjectPermissions.Tasks.Update)]
        public async System.Threading.Tasks.Task<TaskDto> UpdateAsync(Guid id, TaskUpdateDto input)
        {
            var query = await _repository.GetQueryableAsync();
            query = query.Include(t => t.Project);
            query = query.Include(t => t.AssignedTo);
            var entity = await query.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (entity == null) throw new Volo.Abp.Domain.Entities.EntityNotFoundException(typeof(TaskEntity), id);
            entity.Title = input.Title;
            entity.Description = input.Description;
            entity.Status = input.Status;
            entity.DueDate = input.DueDate;
            entity.ProjectId = input.ProjectId;
            entity.AssignedToId = input.AssignedToId;
            await _repository.UpdateAsync(entity);
            var dto = ObjectMapper.Map<TaskEntity, TaskDto>(entity);
            var project = await _projectRepository.GetAsync(input.ProjectId);
            dto.ProjectName = project.Name;
            if (input.AssignedToId.HasValue)
            {
                var employee = await _employeeRepository.GetAsync(input.AssignedToId.Value);
                dto.AssignedToName = $"{employee.FirstName} {employee.LastName}";
            }
            return dto;
        }

        [Authorize(CompanyEmployeeProjectPermissions.Tasks.Delete)]
        public async System.Threading.Tasks.Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
