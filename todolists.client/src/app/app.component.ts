import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface TodoList {
  todoListId: number;
  name: string;
  todos: Todo[];
}

interface Todo {
  todoId: number;
  todoListId: number;
  description: string;
  dueDate: string;
  isImportant: boolean;
  isComplete: boolean;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public todoLists: TodoList[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    //this.getForecasts();
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
