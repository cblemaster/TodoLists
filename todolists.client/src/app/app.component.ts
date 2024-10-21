
import { Component, OnInit } from '@angular/core';
import { TodoList } from '../models/todo-list';
import { HttpDataService } from '../http-data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  // title = 'Todo Lists';

  todoLists : TodoList[] = [];
  selectedTodoList : TodoList = new TodoList;
  
  constructor(private data: HttpDataService) { }
  
  ngOnInit() : void {
    this.getTodoLists();
  }

  setSelectedTodoListId() {}

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

  createTodoList() {

  }

  /* todos : Todo[] | undefined;

  constructor(private data: HttpDataService, private boolYesNo : BoolYesNoPipe) { }

  ngOnInit(): void {
    this.data.readList(6).subscribe(
      (result) => {
        this.todos = result.todos;
      },
      (error) => {
        console.error(error)
      }
    );
  }

  selectedList: TodoList = new TodoList;  

  constructor(private data: HttpDataService) { }
  
  ngOnInit(): void {
    this.getSelectedList(5);
  }

  getSelectedList(id: number) : void {
    this.data.readList(id).subscribe(
      (result) => {
        this.selectedList = result;
      },
      (error) => {
        console.error(error)
      }
    );
  } */
}
