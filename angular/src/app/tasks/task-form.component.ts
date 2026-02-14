import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { TaskService } from '../proxy/tasks/task.service';
import { ProjectService } from '../proxy/projects/project.service';
import { EmployeeService } from '../proxy/employees/employee.service';
import type { ProjectDto } from '../proxy/projects/models';
import type { EmployeeDto } from '../proxy/employees/models';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './task-form.component.html',
})
export class TaskFormComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly taskService = inject(TaskService);
  private readonly projectService = inject(ProjectService);
  private readonly employeeService = inject(EmployeeService);

  form = this.fb.group({
    title: ['', [Validators.required, Validators.maxLength(200)]],
    description: [''],
    status: [0],
    dueDate: [''],
    projectId: ['', Validators.required],
    assignedToId: [''],
  });
  projects: ProjectDto[] = [];
  employees: EmployeeDto[] = [];
  id: string | null = null;
  saving = false;

  ngOnInit(): void {
    this.projectService.getList({ maxResultCount: 1000 }).subscribe((res) => (this.projects = res.items));
    this.employeeService.getList({ maxResultCount: 1000 }).subscribe((res) => (this.employees = res.items));
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) {
      this.taskService.get(this.id).subscribe((dto) => {
        this.form.patchValue({
          title: dto.title,
          description: dto.description ?? '',
          status: dto.status,
          dueDate: dto.dueDate ? dto.dueDate.split('T')[0] : '',
          projectId: dto.projectId ?? '',
          assignedToId: dto.assignedToId ?? '',
        });
      });
    }
  }

  submit(): void {
    if (this.form.invalid || this.saving) return;
    this.saving = true;
    const value = this.form.value as {
      title: string;
      description?: string | null;
      status: number;
      dueDate?: string | null;
      projectId?: string | null;
      assignedToId?: string | null;
    };
    const payload = {
      title: value.title,
      description: value.description || undefined,
      status: value.status,
      dueDate: value.dueDate || undefined,
      projectId: value.projectId || '',
      assignedToId: value.assignedToId || undefined,
    };
    if (this.id) {
      this.taskService.update(this.id, payload).subscribe({
        next: () => this.router.navigate(['/tasks']),
        error: () => (this.saving = false),
      });
    } else {
      this.taskService.create(payload).subscribe({
        next: () => this.router.navigate(['/tasks']),
        error: () => (this.saving = false),
      });
    }
  }
}
