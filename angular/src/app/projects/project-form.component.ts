import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { ProjectService } from '../proxy/projects/project.service';
import { CompanyService } from '../proxy/companies/company.service';
import type { CompanyDto } from '../proxy/companies/models';

@Component({
  selector: 'app-project-form',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink, NgxValidateCoreModule],
  templateUrl: './project-form.component.html',
})
export class ProjectFormComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly projectService = inject(ProjectService);
  private readonly companyService = inject(CompanyService);

  form = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(256)]],
    startDate: [''],
    endDate: [''],
    companyId: [''],
  });
  companies: CompanyDto[] = [];
  id: string | null = null;
  saving = false;

  ngOnInit(): void {
    this.companyService.getList({ maxResultCount: 1000 }).subscribe((res) => (this.companies = res.items));
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) {
      this.projectService.get(this.id).subscribe((dto) => {
        this.form.patchValue({
          name: dto.name,
          startDate: dto.startDate ? dto.startDate.split('T')[0] : '',
          endDate: dto.endDate ? dto.endDate.split('T')[0] : '',
          companyId: dto.companyId ?? '',
        });
      });
    }
  }

  submit(): void {
    if (this.form.invalid || this.saving) return;
    this.saving = true;
    const value = this.form.value as { name: string; startDate?: string | null; endDate?: string | null; companyId?: string | null };
    const payload = {
      name: value.name,
      startDate: value.startDate || undefined,
      endDate: value.endDate || undefined,
      companyId: value.companyId || undefined,
    };
    if (this.id) {
      this.projectService.update(this.id, payload).subscribe({
        next: () => this.router.navigate(['/projects']),
        error: () => (this.saving = false),
      });
    } else {
      this.projectService.create(payload).subscribe({
        next: () => this.router.navigate(['/projects']),
        error: () => (this.saving = false),
      });
    }
  }
}
