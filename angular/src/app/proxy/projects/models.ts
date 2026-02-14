import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface ProjectCreateDto {
  name: string;
  startDate?: string | null;
  endDate?: string | null;
  companyId?: string;
}

export interface ProjectDto extends AuditedEntityDto<string> {
  name?: string;
  startDate?: string | null;
  endDate?: string | null;
  companyId?: string;
  companyName?: string | null;
}

export interface ProjectGetListInput extends PagedAndSortedResultRequestDto {
  filter?: string | null;
  companyId?: string | null;
}

export interface ProjectUpdateDto {
  name: string;
  startDate?: string | null;
  endDate?: string | null;
  companyId?: string;
}
