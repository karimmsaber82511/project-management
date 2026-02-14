import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NgClass } from '@angular/common';
import { TaskService } from '../proxy/tasks/task.service';
import { PagedResultDto, PermissionDirective } from '@abp/ng.core';
import type { TaskDto, TaskGetListInput } from '../proxy/tasks/models';
import { permissions } from '../permissions';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [RouterLink, NgClass, PermissionDirective],
  templateUrl: './task-list.component.html',
})
export class TaskListComponent implements OnInit {
  protected readonly permissions = permissions;
  private readonly taskService = inject(TaskService);
  list: PagedResultDto<TaskDto> = { items: [], totalCount: 0 };
  input: TaskGetListInput = { maxResultCount: 10, skipCount: 0, sorting: 'creationTime' };
  loading = false;

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading = true;
    this.taskService.getList(this.input).subscribe({
      next: (res) => {
        this.list = res;
        this.loading = false;
      },
      error: () => (this.loading = false),
    });
  }

  delete(id: string): void {
    if (confirm('Delete this task?')) {
      this.taskService.delete(id).subscribe(() => this.load());
    }
  }

  pageChanged(skipCount: number): void {
    this.input.skipCount = skipCount;
    this.load();
  }

  formatDate(value: string | null | undefined): string {
    if (!value) return '-';
    try {
      return new Date(value).toLocaleDateString();
    } catch {
      return value;
    }
  }

  getStatusLabel(status: number): string {
    switch (status) {
      case 0: return 'To Do';
      case 1: return 'In Progress';
      case 2: return 'Done';
      default: return 'Unknown';
    }
  }
}
