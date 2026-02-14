import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface TaskCreateDto {
  title: string;
  description?: string | null;
  status: number;
  dueDate?: string | null;
  projectId?: string;
  assignedToId?: string | null;
}

export interface TaskDto extends AuditedEntityDto<string> {
  title?: string;
  description?: string | null;
  status: number;
  dueDate?: string | null;
  projectId?: string;
  projectName?: string | null;
  assignedToId?: string | null;
  assignedToName?: string | null;
}

export interface TaskGetListInput extends PagedAndSortedResultRequestDto {
  filter?: string | null;
  projectId?: string | null;
  assignedToId?: string | null;
  status?: number | null;
}

export interface TaskUpdateDto {
  title: string;
  description?: string | null;
  status: number;
  dueDate?: string | null;
  projectId?: string;
  assignedToId?: string | null;
}
