import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { EmployeeService } from '../proxy/employees/employee.service';
import { PagedResultDto, PermissionDirective } from '@abp/ng.core';
import type { EmployeeDto, EmployeeGetListInput } from '../proxy/employees/models';
import { permissions } from '../permissions';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [RouterLink, PermissionDirective],
  templateUrl: './employee-list.component.html',
})
export class EmployeeListComponent implements OnInit {
  protected readonly permissions = permissions;
  private readonly employeeService = inject(EmployeeService);
  list: PagedResultDto<EmployeeDto> = { items: [], totalCount: 0 };
  input: EmployeeGetListInput = { maxResultCount: 10, skipCount: 0, sorting: 'lastName' };
  loading = false;

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading = true;
    this.employeeService.getList(this.input).subscribe({
      next: (res) => {
        this.list = res;
        this.loading = false;
      },
      error: () => (this.loading = false),
    });
  }

  delete(id: string): void {
    if (confirm('Delete this employee?')) {
      this.employeeService.delete(id).subscribe(() => this.load());
    }
  }

  pageChanged(skipCount: number): void {
    this.input.skipCount = skipCount;
    this.load();
  }
}
