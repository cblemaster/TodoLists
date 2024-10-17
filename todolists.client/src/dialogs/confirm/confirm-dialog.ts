import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialog, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle } from '@angular/material/dialog';

@Component({
  styleUrls: ['confirm-dialog.css'],
  selector: 'confirm-dialog',
  templateUrl: 'confirm-dialog.html',
  standalone: true,
  imports: [MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MatButtonModule],
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class ConfirmDialog {
  readonly dialog = inject(MatDialog);
  action: string = '';
  message: string = '';
  data = inject(MAT_DIALOG_DATA);

    openDialog() {
    this.dialog.open(ConfirmDialog);
    }
}
