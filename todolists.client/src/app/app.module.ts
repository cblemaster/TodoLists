import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { TodolistComponent } from './todolist/todolist.component';
import { BoolYesNoPipe } from '../shared/pipes/bool-yes-no.pipe';

@NgModule({ declarations: [
        AppComponent,
        TodolistComponent,
        BoolYesNoPipe,
    ],
    bootstrap: [AppComponent], imports: [BrowserModule], providers: [provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
