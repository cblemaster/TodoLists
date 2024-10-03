import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TodoList } from '../shared/models/todoList';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public todoLists: TodoList[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getTodoLists();
    }

  getTodoLists() {
    this.http.get<TodoList[]>('/todolist').subscribe(
      (result) => {
        this.todoLists = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'Todo Lists';
}
