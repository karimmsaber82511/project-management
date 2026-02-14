import { Routes } from '@angular/router';
import { permissionGuard } from '@abp/ng.core';
import { EmployeeListComponent } from './employee-list.component';
import { EmployeeFormComponent } from './employee-form.component';
import { permissions } from '../permissions';

export const employeesRoutes: Routes = [
  {
    path: '',
    component: EmployeeListComponent,
    canActivate: [permissionGuard],
    data: { requiredPolicy: permissions.employees.default },
  },
  {
    path: 'create',
    component: EmployeeFormComponent,
    canActivate: [permissionGuard],
    data: { requiredPolicy: permissions.employees.create },
  },
  {
    path: ':id/edit',
    component: EmployeeFormComponent,
    canActivate: [permissionGuard],
    data: { requiredPolicy: permissions.employees.update },
  },
];
