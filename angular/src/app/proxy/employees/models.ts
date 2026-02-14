import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface EmployeeCreateDto {
  firstName: string;
  lastName: string;
  email?: string | null;
  companyId?: string;
}

export interface EmployeeDto extends AuditedEntityDto<string> {
  firstName?: string;
  lastName?: string;
  email?: string | null;
  companyId?: string;
  companyName?: string | null;
}

export interface EmployeeGetListInput extends PagedAndSortedResultRequestDto {
  filter?: string | null;
  companyId?: string | null;
}

export interface EmployeeUpdateDto {
  firstName: string;
  lastName: string;
  email?: string | null;
  companyId?: string;
}
