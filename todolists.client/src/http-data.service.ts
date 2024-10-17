import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TodoList } from './models/todo-list'
import { Todo } from './models/todo';

@Injectable({
  providedIn: 'root'
})

export class HttpDataService {
  constructor(private httpClient: HttpClient) { }
    
  public createList(todoList: TodoList): Observable<TodoList> {
    return this.httpClient.post<TodoList>('/todolist', todoList);
  }
  public readList(id: number): Observable<TodoList> {
    return this.httpClient.get<TodoList>(`todolist/${id}`);
  }
  public listList(): Observable<TodoList[]> {
    return this.httpClient.get<TodoList[]>('/todolist');
  }
  public deleteList(id: number) { 
    return this.httpClient.delete(`todolist/${id}`);
  }
  public updateList(todoList: TodoList): Observable<TodoList> {
    return this.httpClient.put<TodoList>(`todolist/${todoList.todoListId}`, todoList);
  }

  public createTodo(todo: Todo): Observable<Todo> {
    return this.httpClient.post<Todo>('/todo', todo);
  }
  public readTodo(id: number): Observable<Todo> {
    return this.httpClient.get<Todo>(`todo/${id}`);
  }
  public deleteTodo(id: number) { 
    return this.httpClient.delete(`todo/${id}`);
  }
  public updateTodo(todo: Todo): Observable<Todo> {
    return this.httpClient.put<Todo>(`todo/${todo.todoId}`, todo);
  }
}
