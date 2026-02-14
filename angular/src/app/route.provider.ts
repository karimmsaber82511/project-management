import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { permissions } from './permissions';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/companies',
        name: 'CompanyEmployeeProject::Companies',
        iconClass: 'fas fa-building',
        order: 2,
        layout: eLayoutType.application,
        requiredPolicy: permissions.companies.default,
      },
      {
        path: '/employees',
        name: 'CompanyEmployeeProject::Employees',
        iconClass: 'fas fa-users',
        order: 3,
        layout: eLayoutType.application,
        requiredPolicy: permissions.employees.default,
      },
      {
        path: '/projects',
        name: 'CompanyEmployeeProject::Projects',
        iconClass: 'fas fa-project-diagram',
        order: 4,
        layout: eLayoutType.application,
        requiredPolicy: permissions.projects.default,
      },
    ]);
  };
}
