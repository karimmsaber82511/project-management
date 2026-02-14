import { Routes } from '@angular/router';
import { permissionGuard } from '@abp/ng.core';
import { CompanyListComponent } from './company-list.component';
import { CompanyFormComponent } from './company-form.component';
import { permissions } from '../permissions';

export const companiesRoutes: Routes = [
  {
    path: '',
    component: CompanyListComponent,
    canActivate: [permissionGuard],
    data: { requiredPolicy: permissions.companies.default },
  },
  {
    path: 'create',
    component: CompanyFormComponent,
    canActivate: [permissionGuard],
    data: { requiredPolicy: permissions.companies.create },
  },
  {
    path: ':id/edit',
    component: CompanyFormComponent,
    canActivate: [permissionGuard],
    data: { requiredPolicy: permissions.companies.update },
  },
];
