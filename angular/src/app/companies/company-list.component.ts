import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CompanyService } from '../proxy/companies/company.service';
import { PagedResultDto, PermissionDirective } from '@abp/ng.core';
import { LocalizationPipe } from '@abp/ng.core';
import type { CompanyDto, CompanyGetListInput } from '../proxy/companies/models';
import { permissions } from '../permissions';

@Component({
  selector: 'app-company-list',
  standalone: true,
  imports: [RouterLink, LocalizationPipe, PermissionDirective],
  templateUrl: './company-list.component.html',
})
export class CompanyListComponent implements OnInit {
  protected readonly permissions = permissions;
  private readonly companyService = inject(CompanyService);
  list: PagedResultDto<CompanyDto> = { items: [], totalCount: 0 };
  input: CompanyGetListInput = { maxResultCount: 10, skipCount: 0, sorting: 'name' };
  loading = false;

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading = true;
    this.companyService.getList(this.input).subscribe({
      next: (res) => {
        this.list = res;
        this.loading = false;
      },
      error: () => (this.loading = false),
    });
  }

  delete(id: string): void {
    if (confirm('Delete this company?')) {
      this.companyService.delete(id).subscribe(() => this.load());
    }
  }

  pageChanged(skipCount: number): void {
    this.input.skipCount = skipCount;
    this.load();
  }
}
