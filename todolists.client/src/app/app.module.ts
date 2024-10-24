import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpDataService } from '../http-data.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule(
  { declarations: [ AppComponent ],
    imports: [ BrowserModule, AppRoutingModule, HttpClientModule ],
    providers: [ HttpDataService ],
    bootstrap: [AppComponent ] })

export class AppModule { }
