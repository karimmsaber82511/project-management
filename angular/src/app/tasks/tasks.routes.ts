import { Routes } from '@angular/router';
import { permissionGuard } from '@abp/ng.core';
import { TaskListComponent } from './task-list.component';
import { TaskFormComponent } from './task-form.component';
import { permissions } from '../permissions';

export const tasksRoutes: Routes = [
  {
    path: '',
    component: TaskListComponent,
    canActivate: [permissionGuard],
    data: { requiredPolicy: permissions.tasks.default },
  },
  {
    path: 'create',
    component: TaskFormComponent,
    canActivate: [permissionGuard],
    data: { requiredPolicy: permissions.tasks.create },
  },
  {
    path: ':id/edit',
    component: TaskFormComponent,
    canActivate: [permissionGuard],
    data: { requiredPolicy: permissions.tasks.update },
  },
];
