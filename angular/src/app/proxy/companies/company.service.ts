import type { CompanyCreateDto, CompanyDto, CompanyGetListInput, CompanyUpdateDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable, inject } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CompanyService {
  private restService = inject(RestService);
  apiName = 'Default';
  

  create = (input: CompanyCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CompanyDto>({
      method: 'POST',
      url: '/api/app/company',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/company/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CompanyDto>({
      method: 'GET',
      url: `/api/app/company/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: CompanyGetListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CompanyDto>>({
      method: 'GET',
      url: '/api/app/company',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CompanyUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CompanyDto>({
      method: 'PUT',
      url: `/api/app/company/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
}