import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TodoListsComponent } from './todo-lists/todo-lists.component';
import { HttpDataService } from '../http-data.service';
import { HttpClientModule } from '@angular/common/http';
import { SelectedListComponent } from './selected-list/selected-list.component';

@NgModule(
  { declarations: [ AppComponent, TodoListsComponent, SelectedListComponent ],
    imports: [ BrowserModule, AppRoutingModule, HttpClientModule ],
    providers: [ HttpDataService ],
    bootstrap: [AppComponent ] })

export class AppModule { }
