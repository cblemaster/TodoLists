import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BoolYesNoPipe } from './pipes/bool-yes-no.pipe';
import { Todo } from './models/todo';
import { TodoList } from './models/todo-list';

@NgModule({ declarations: [BoolYesNoPipe, Todo, TodoList], imports: [CommonModule] })

export class SharedModule { }
