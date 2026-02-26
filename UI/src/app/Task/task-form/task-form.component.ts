import { Component, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { Task } from '../../Model/task';
 
@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html'
})
export class TaskFormComponent implements OnChanges {
  @Input() editData: any = null;
  @Output() onSave = new EventEmitter<Task>();
  task: Task = this.create();
  ngOnChanges() {
    if (this.editData) this.task = { ...this.editData };
  }
 
  create(): Task {
    return { taskTitle: '', taskDescription: '', taskDueDate: '', taskStatus: 'Pending', taskRemarks: '', createdByName: 'Admin_User' };
  }
 
  submit() {
    this.onSave.emit(this.task);
    this.task = this.create();
  }
}