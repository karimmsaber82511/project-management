import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { CompanyService } from '../proxy/companies/company.service';

@Component({
  selector: 'app-company-form',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink, NgxValidateCoreModule],
  templateUrl: './company-form.component.html',
})
export class CompanyFormComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly companyService = inject(CompanyService);

  form = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(256)]],
    address: [''],
  });
  id: string | null = null;
  saving = false;

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id) {
      this.companyService.get(this.id).subscribe((dto) => this.form.patchValue(dto));
    }
  }

  submit(): void {
    if (this.form.invalid || this.saving) return;
    this.saving = true;
    const value = this.form.value as { name: string; address?: string | null };
    if (this.id) {
      this.companyService.update(this.id, value).subscribe({
        next: () => this.router.navigate(['/companies']),
        error: () => (this.saving = false),
      });
    } else {
      this.companyService.create(value).subscribe({
        next: () => this.router.navigate(['/companies']),
        error: () => (this.saving = false),
      });
    }
  }
}
