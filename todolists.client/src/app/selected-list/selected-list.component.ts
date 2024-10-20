import { Component } from '@angular/core';
import { TodoList } from '../../models/todo-list';

@Component({
  selector: 'app-selected-list',
  templateUrl: './selected-list.component.html',
  styleUrl: './selected-list.component.css'
})

export class SelectedListComponent {
  selectedList : TodoList | undefined;


}
