
import { Component, OnInit } from '@angular/core';
import { TodoList } from '../models/todo-list';
import { HttpDataService } from '../http-data.service';
import { Todo } from '../models/todo';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmDeleteModalComponent } from '../dialogs/confirm-delete-modal/confirm-delete-modal.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit {

  todoLists : TodoList[] = [];
  selectedTodoList! : TodoList;
  todos : Todo[] = [];
  
  constructor(private data: HttpDataService, private modalService: NgbModal) { }
  
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
    // todo > confirmation dialog
    
    this.data.deleteTodo(id).subscribe(
      (result) => { return },
      (error) => { console.error(error) }
    );

    // todo > success dialog, refresh selectedtodolist
  }

  deleteSelectedTodoList(): void {
    this.modalService.open(ConfirmDeleteModalComponent);
    if (this.selectedTodoList.todos.length > 0) {
      // todo > todos must be deleted first
    }
    else {
      this.data.deleteList(this.selectedTodoList.todoListId).subscribe(
        (result) => { return },
        (error) => { console.error(error) }
      );
    }
    // todo > succedss dialog, reset selectedtodolist
  }

  createTodoList() {

  }
}
