import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { TodolistComponent } from './todolist/todolist.component';
import { CreatetodolistComponent } from './createtodolist/createtodolist.component';
import { RenametodolistComponent } from './renametodolist/renametodolist.component';
import { DeletetodolistComponent } from './deletetodolist/deletetodolist.component';
import { CreatetodoComponent } from './createtodo/createtodo.component';
import { UpdatetodoComponent } from './updatetodo/updatetodo.component';
import { TodoComponent } from './todo/todo.component';
import { DeletetodoComponent } from './deletetodo/deletetodo.component';

@NgModule({ declarations: [
        AppComponent,
        TodolistComponent,
        CreatetodolistComponent,
        RenametodolistComponent,
        DeletetodolistComponent,
        CreatetodoComponent,
        UpdatetodoComponent,
        TodoComponent,
        DeletetodoComponent
    ],
    bootstrap: [AppComponent], imports: [BrowserModule], providers: [provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
