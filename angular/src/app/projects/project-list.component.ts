import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ProjectService } from '../proxy/projects/project.service';
import { PagedResultDto, PermissionDirective } from '@abp/ng.core';
import type { ProjectDto, ProjectGetListInput } from '../proxy/projects/models';
import { permissions } from '../permissions';

@Component({
  selector: 'app-project-list',
  standalone: true,
  imports: [RouterLink, PermissionDirective],
  templateUrl: './project-list.component.html',
})
export class ProjectListComponent implements OnInit {
  protected readonly permissions = permissions;
  private readonly projectService = inject(ProjectService);
  list: PagedResultDto<ProjectDto> = { items: [], totalCount: 0 };
  input: ProjectGetListInput = { maxResultCount: 10, skipCount: 0, sorting: 'name' };
  loading = false;

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading = true;
    this.projectService.getList(this.input).subscribe({
      next: (res) => {
        this.list = res;
        this.loading = false;
      },
      error: () => (this.loading = false),
    });
  }

  delete(id: string): void {
    if (confirm('Delete this project?')) {
      this.projectService.delete(id).subscribe(() => this.load());
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
}
