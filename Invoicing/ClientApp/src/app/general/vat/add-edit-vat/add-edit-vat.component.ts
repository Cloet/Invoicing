import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '../../../common/base.component';
import { VAT } from '../../../common/model/vat.model';
import { VATService } from '../../../common/service/vat.service';
import { DeleteVatDialogComponent } from '../delete-vat-dialog/delete-vat-dialog.component';

@Component({
  selector: 'app-add-edit-vat',
  templateUrl: './add-edit-vat.component.html',
  styleUrls: ['./add-edit-vat.component.scss']
})
export class AddEditVatComponent extends BaseComponent implements OnInit {

  @Input() vat!: VAT;
  vatForm!: FormGroup;
  isAddMode!: boolean;
  id!: string;

  constructor(
    private _fb: FormBuilder,
    private _vatService: VATService,
    private _router: Router,
    private _dialog: MatDialog,
    private _snackbar: MatSnackBar,
    private _route: ActivatedRoute
  ) {
    super(_dialog);
  }

  private createFormGroup(formBuilder: FormBuilder): FormGroup {
    return formBuilder.group({
      vat: formBuilder.group({
        id: [''],
        code: [
          '',
          [
            Validators.required,
            Validators.minLength(1)
          ]
        ],
        percentage: [
          '',
          [
            Validators.required,
            Validators.pattern('[0-9]*')
          ]
        ],
        description: [
          ''
        ]
      })
    })
  }

  ngOnInit(): void {
    this.id = this._route.snapshot.params['id'];
    this.isAddMode = !this.id

    this.subscribeToErrors<VAT>(this._vatService);

    this.vatForm = this.createFormGroup(this._fb);
    this.vat = new VAT();

    if (!this.isAddMode) {
      this._vatService.getVATForId$(Number(this.id)).subscribe(val => {
        this.vat = val;
        this.vatForm.get('vat')?.patchValue(val);
      });
    }

  }

  onSubmit() {
    if (this.vatForm?.valid) {
      this.vat.updatePartial(this.vatForm.getRawValue().vat);
      if (this.isAddMode) {
        this.addVAT();
      } else {
        this.updateVAT();
      }
    }
  }

  addVAT() {
    if (this.vatForm?.valid) {
      this._vatService.createVAT$(this.vat).subscribe(
        response => {
          if (response.id != null && response.id > 0) {
            this._snackbar.open("VAT has been created.", 'ok', { duration: 2000 });
            this._router.navigate(['/vat']);
          }
        }
      );
    }
  }

  updateVAT() {
    if (this.vatForm?.valid) {
      this._vatService.updateVAT$(this.vat).subscribe(res => {
        this._snackbar.open("VAT has been saved", 'ok', { duration: 2000 });
      });
    }
  }

  deleteVAT() {
    this._vatService.getVATForId$(this.vat.id).subscribe(val => {
      this.vat = val;

      if (this.vat.id === undefined || this.vat.id <= 0) {
        this.showErrorDialog("Cannot delete vat, entry not found.");
        this._router.navigate(['/vat']);
        return;
      }

      const dialogConfig = new MatDialogConfig();
      dialogConfig.autoFocus = true;
      const dialogRef = this._dialog.open(DeleteVatDialogComponent, dialogConfig);

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this._vatService.deleteVAT(this.vat.id).subscribe(res => {
            this._snackbar.open("VAT has been deleted", 'ok', { duration: 2000 });
            this._router.navigate(['/vat']);
          })
        }
      });

    })
  }

}
