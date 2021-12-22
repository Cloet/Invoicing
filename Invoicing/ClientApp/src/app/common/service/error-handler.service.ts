import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ErrorDialogComponent } from '../../error-dialog/error-dialog.component';
import { IError } from '../model/errors.model';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  constructor(public dialog: MatDialog) { }

  showErrorDialog(errors: IError[]) {
    console.log('inside error handler service')
    console.log(errors)
    const dialogConfig = new MatDialogConfig()
    dialogConfig.autoFocus = true
    dialogConfig.data = {
      title: "Error",
      errors: errors
    }


    this.dialog.afterAllClosed

    const dialogRef = this.dialog.open(ErrorDialogComponent, dialogConfig)

    return dialogRef.afterClosed()
  }
}
