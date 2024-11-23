
import { Component, Inject, inject, Input, Output } from '@angular/core';

@Component({
	selector: 'app-confirm-delete-modal',
  templateUrl: './confirm-delete-modal.component.html',
  styleUrl: './confirm-delete-modal.component.css'
})

export class ConfirmDeleteModalComponent {
  @Input() itemName : string = '';
  @Output() deleteConfirmed : boolean = false;

  cancel() {
    this.deleteConfirmed.emit(false);
  }
  
  confirm() {
    this.deleteConfirmed.emit(true);
  }
}
