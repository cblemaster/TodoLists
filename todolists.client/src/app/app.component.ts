
import { Component, OnInit } from '@angular/core';
import { TodoList } from '../models/todo-list';
import { HttpDataService } from '../http-data.service';
import { Todo } from '../models/todo';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit {

  todoLists : TodoList[] = [];
  selectedTodoList! : TodoList;
  todos : Todo[] = [];
  
  constructor(private data: HttpDataService) { }
  
  ngOnInit() : void {
    this.getTodoLists();
  }

  getTodoLists() : void {
    this.data.listList().subscribe(
      (result) => {
        this.todoLists = result;
      },
      (error) => {
        console.error(error)
      }
    );
  }

  setSelectedTodoList(id : number) : void {
    this.data.readList(id).subscribe(
      (result) => {
        this.selectedTodoList = result;
        this.todos = result.todos;
      },
      (error) => {
        console.error(error)
      }
    );
  }

  deleteTodo(id: number): void {
    this.data.deleteTodo(id).subscribe(
      (result) => { return },
      (error) => { console.error(error) }
    );
  }

  
  createTodoList() {

  }
}
