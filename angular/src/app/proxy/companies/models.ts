import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CompanyCreateDto {
  name: string;
  address?: string | null;
}

export interface CompanyDto extends AuditedEntityDto<string> {
  name?: string;
  address?: string | null;
}

export interface CompanyGetListInput extends PagedAndSortedResultRequestDto {
  filter?: string | null;
}

export interface CompanyUpdateDto {
  name: string;
  address?: string | null;
}
