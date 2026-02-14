import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { EmployeeService } from '../proxy/employees/employee.service';
import { CompanyService } from '../proxy/companies/company.service';
import type { CompanyDto } from '../proxy/companies/models';

@Component({
  selector: 'app-employee-form',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink, NgxValidateCoreModule],
  templateUrl: './employee-form.component.html',
})
export class EmployeeFormComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly employeeService = inject(EmployeeService);
  private readonly companyService = inject(CompanyService);

  form = this.fb.group({
    firstName: ['', [Validators.required, Validators.maxLength(256)]],
    lastName: ['', [Validators.required, Validators.maxLength(256)]],
    email: [''],
    companyId: [''],
  });
  companies: CompanyDto[] = [];
  id: string | null = null;
  saving = false;

  ngOnInit(): void {
    this.companyService.getList({ maxResultCount: 1000 }).subscribe((res) => (this.companies = res.items));
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) {
      this.employeeService.get(this.id).subscribe((dto) => this.form.patchValue(dto));
    }
  }

  submit(): void {
    if (this.form.invalid || this.saving) return;
    this.saving = true;
    const value = this.form.value as { firstName: string; lastName: string; email?: string | null; companyId?: string | null };
    const payload = {
      ...value,
      companyId: value.companyId || undefined,
    };
    if (this.id) {
      this.employeeService.update(this.id, payload).subscribe({
        next: () => this.router.navigate(['/employees']),
        error: () => (this.saving = false),
      });
    } else {
      this.employeeService.create(payload).subscribe({
        next: () => this.router.navigate(['/employees']),
        error: () => (this.saving = false),
      });
    }
  }
}
