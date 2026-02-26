import { Component, OnInit } from '@angular/core';
import { Task } from 'src/app/Model/task';
import { TaskService } from 'src/app/Services/task.service';
 
@Component({
  selector: 'app-task-dashboard',
  templateUrl: './task-dashboard.component.html'
})
export class TaskDashboardComponent implements OnInit {
  tasks: Task[] = [];
  searchKey: string = '';
  selectedForEdit: any = null;
 
  constructor(private service: TaskService) {}
 
  ngOnInit() { this.refresh(); }
 
  refresh() {
    this.service.getTasks(this.searchKey).subscribe(res => this.tasks = res);
  }
 
  handleSave(task: Task) {
    if (task.taskId) {
      this.service.updateTask(task.taskId, task).subscribe(() => { this.refresh(); this.selectedForEdit = null; });
    } else {
      this.service.createTask(task).subscribe(() => this.refresh());
    }
  }
 
  onDelete(id: any) {
    if(confirm("Delete this task?")) this.service.deleteTask(id).subscribe(() => this.refresh());
  }
}