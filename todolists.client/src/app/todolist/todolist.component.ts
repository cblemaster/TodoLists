import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TodoList } from 'src/shared/models/todoList'
import { Todo } from '../../shared/models/todo';

@Component({
  selector: 'app-todolist',
  templateUrl: './todolist.component.html',
  styleUrls: ['./todolist.component.css']
})
export class TodolistComponent implements OnInit {
  public todoLists: TodoList[] = [];
  public selectedTodoList!: TodoList;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getTodoLists();
    this.getSelectedTodoList(5);
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

  getSelectedTodoList(id: number) {
    this.http.get<TodoList>('/todolist/' + id).subscribe(
      {
        next: (result) => {
          this.selectedTodoList = result;
        },
        error: (error) => {
          console.error(error);
        }
      }
    );
  }

  title = 'Todo Lists';
}
