import { Component, OnInit } from '@angular/core';
import { HttpDataService } from '../../http-data.service';
import { TodoList } from '../../models/todo-list';

@Component({
  selector: 'app-todo-lists',
  templateUrl: './todo-lists.component.html',
  styleUrl: './todo-lists.component.css'
})
export class TodoListsComponent implements OnInit {
  public todoLists : TodoList[] = [];
  
  constructor(private data: HttpDataService) { }
  
  ngOnInit() {
    this.getTodoLists();
  }

  getTodoLists() {
    this.data.listList().subscribe(
      (result) => {
        this.todoLists = result;
      },
      (error) => {
        console.error(error)
      }
    );
  }

  selectList() {

  }
}
