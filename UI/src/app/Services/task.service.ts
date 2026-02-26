// import { Injectable } from '@angular/core';

// @Injectable({
//   providedIn: 'root'
// })
// export class TaskService {

//   constructor() { }
// }

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../Model/task'
 
@Injectable({ providedIn: 'root' })
export class TaskService {
  private apiUrl = 'https://localhost:44312/api/Task';
 
  constructor(private http: HttpClient) {}
 
  getTasks(search: string = ''): Observable<Task[]> {
    return this.http.get<Task[]>(`${this.apiUrl}?search=${search}`);
  }
 
  createTask(task: Task): Observable<any> {
    return this.http.post(this.apiUrl, task);
  }
 
  updateTask(id: number, task: Task): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, task);
  }
 
  deleteTask(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}