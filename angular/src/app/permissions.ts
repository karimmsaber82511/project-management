/**
 * Permission names matching backend CompanyEmployeeProjectPermissions.
 * @see https://abp.io/docs/latest/framework/ui/angular/permission-management
 */
export const permissions = {
  companies: {
    default: 'CompanyEmployeeProject.Companies',
    create: 'CompanyEmployeeProject.Companies.Create',
    update: 'CompanyEmployeeProject.Companies.Update',
    delete: 'CompanyEmployeeProject.Companies.Delete',
  },
  employees: {
    default: 'CompanyEmployeeProject.Employees',
    create: 'CompanyEmployeeProject.Employees.Create',
    update: 'CompanyEmployeeProject.Employees.Update',
    delete: 'CompanyEmployeeProject.Employees.Delete',
  },
  projects: {
    default: 'CompanyEmployeeProject.Projects',
    create: 'CompanyEmployeeProject.Projects.Create',
    update: 'CompanyEmployeeProject.Projects.Update',
    delete: 'CompanyEmployeeProject.Projects.Delete',
  },
} as const;
