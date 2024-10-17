import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialog, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle } from '@angular/material/dialog';

@Component({
  styleUrls: ['error-dialog.css'],
  selector: 'error-dialog',
  templateUrl: 'error-dialog.html',
  standalone: true,
  imports: [MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MatButtonModule, CommonModule],
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class ErrorDialog {
  readonly dialog = inject(MatDialog);
  errors : string[] = [];
  data = inject(MAT_DIALOG_DATA);

    openDialog() {
    this.dialog.open(ErrorDialog);
    }
}
