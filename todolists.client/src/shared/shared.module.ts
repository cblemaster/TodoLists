import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BoolYesNoPipe } from './pipes/bool-yes-no.pipe';
import { ConfirmDialog } from './dialogs/confirm/confirm-dialog';
import { ErrorDialog } from './dialogs/error/error-dialog';

@NgModule({ declarations: [BoolYesNoPipe ], imports: [CommonModule, ConfirmDialog, ErrorDialog] })

export class SharedModule { }
