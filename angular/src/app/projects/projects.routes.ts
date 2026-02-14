import { Routes } from '@angular/router';
import { permissionGuard } from '@abp/ng.core';
import { ProjectListComponent } from './project-list.component';
import { ProjectFormComponent } from './project-form.component';
import { permissions } from '../permissions';

export const projectsRoutes: Routes = [
  {
    path: '',
    component: ProjectListComponent,
    canActivate: [permissionGuard],
    data: { requiredPolicy: permissions.projects.default },
  },
  {
    path: 'create',
    component: ProjectFormComponent,
    canActivate: [permissionGuard],
    data: { requiredPolicy: permissions.projects.create },
  },
  {
    path: ':id/edit',
    component: ProjectFormComponent,
    canActivate: [permissionGuard],
    data: { requiredPolicy: permissions.projects.update },
  },
];
