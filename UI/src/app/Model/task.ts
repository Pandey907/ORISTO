export interface Task {
  taskId?: number;
  taskTitle?: string;
  taskDescription?: string;
  taskDueDate?: string;
  taskStatus?: string;
  taskRemarks?: string;
  createdOn?: Date;
  lastUpdatedOn?: Date;
  createdByName?: string;
  lastUpdatedByName?: string;
}

