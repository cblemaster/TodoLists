import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeleteListComponent } from './list/delete-list/delete-list.component';
import { ListComponent } from './list/list/list.component';
import { ListsComponent } from './list/lists/lists.component';
import { InputListComponent } from './list/input-list/input-list.component';
import { DeleteTodoComponent } from './todo/delete-todo/delete-todo.component';
import { TodoComponent } from './todo/todo/todo.component';
import { TodosComponent } from './todo/todos/todos.component';
import { InputTodoComponent } from './todo/input-todo/input-todo.component';

@NgModule({
  declarations: [ 
    DeleteListComponent, 
    ListComponent,
    ListsComponent,
    InputListComponent,
    DeleteTodoComponent,
    TodoComponent,
    TodosComponent,
    InputTodoComponent],
  imports: [ CommonModule ]
})
export class TodoComponentModule { }
