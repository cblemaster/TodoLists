import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TodoList } from 'src/shared/models/todoList'

@Component({
  selector: 'app-todolist',
  templateUrl: './todolist.component.html',
  styleUrls: ['./todolist.component.css']
})
export class TodolistComponent implements OnInit {
  public todoLists: TodoList[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getTodoLists();
  }

  getTodoLists() {
    this.http.get<TodoList[]>('/todolist').subscribe(
      {
        next: (result) => {
          this.todoLists = result;
        },
      error: (error) => {
      console.error(error);
    }
      }
    );
  }

  title = 'Todo Lists';
}
