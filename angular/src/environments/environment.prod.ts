import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4242';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'CompanyEmployeeProject',
    logoUrl: '',
  },
  localization: {
    defaultResourceName: 'CompanyEmployeeProject',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44305/',
    redirectUri: baseUrl,
    clientId: 'CompanyEmployeeProject_App',
    responseType: 'code',
    scope: 'offline_access CompanyEmployeeProject',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44305',
      rootNamespace: 'CompanyEmployeeProject',
    },
  },
} as Environment;
