import { AbstractControl } from "@angular/forms";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { ErrorDialogComponent } from "../error-dialog/error-dialog.component";
import { BaseService } from "./service/base.service";

export abstract class BaseComponent {

  constructor(
    private _errordialog: MatDialog
  ) {

  }

  showErrorDialog(errors: string) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      title: 'Error',
      errors
    };
    const dialogRef = this._errordialog.open(ErrorDialogComponent, dialogConfig);

    return dialogRef.afterClosed();
  }

  getErrorMessage(control: AbstractControl) {
    if (control == null)
      return ''

    for (const err in control.errors) {
      if (control.touched && control.errors.hasOwnProperty(err)) {
        return this.getErrorMessageText(err, control.errors[err]);
      }
    }
    return '';
  }

  getErrorMessageText(errorName: string, errorvalue?: any) {
    let dict = new Map<string, string>();

    dict.set('required', "Required");
    dict.set('minlength', `Min. amount of characters ${errorvalue.requiredLength}`)

    if (errorName === 'pattern' && errorvalue.requiredPattern === "^[A-Z _-]*$") {
      dict.set('pattern', 'Only CAPITAL letters are allowed.')
    } else {
      dict.set('pattern', 'Only letters and digits are allowed')
    }

    return dict.get(errorName);
  }

  subscribeToErrors(service: BaseService) {

    service.loadingError$.subscribe(
      err => {
        this.showErrorDialog(err);
      }
    )

    service.putError$.subscribe(
      err => {
        this.showErrorDialog(err);
      }
    )

    service.postError$.subscribe(
      err => {
        this.showErrorDialog(err);
      }
    )

  }



}
