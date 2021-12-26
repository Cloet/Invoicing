import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '../../../common/base.component';
import { VAT } from '../../../common/model/vat.model';
import { VATService } from '../../../common/service/vat.service';
import { DeleteVatDialogComponent } from '../delete-vat-dialog/delete-vat-dialog.component';

@Component({
  selector: 'app-edit-vat',
  templateUrl: './edit-vat.component.html',
  styleUrls: ['./edit-vat.component.scss']
})
export class EditVatComponent extends BaseComponent implements OnInit {

  @Input() vat!: VAT;
  @Output() updated = new EventEmitter<boolean>();
  vatForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private _vatService: VATService,
    private _router: Router,
    private _route: ActivatedRoute,
    private _dialog: MatDialog
  ) {
    super(_dialog);
  }

  ngOnInit(): void {
    this.subscribeToErrors<VAT>(this._vatService);

    this.vatForm = this.fb.group({
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
    });
    this.vat = new VAT();
    this.vatForm.controls.code.disable();


    let id = this._route.snapshot.paramMap.get('id');
    if (id != null) {
      this._vatService.getVATForId$(Number(id)).subscribe(
        res => {
          this.vat = res;
          this.vatForm.controls.code.setValue(res.code);
          this.vatForm.controls.percentage.setValue(res.percentage);
          this.vatForm.controls.description.setValue(res.description);
        }
      );
    }
  }

  onSubmit() {
    if (this.vatForm?.valid) {
      this.vat.code = this.vatForm.get('code')?.value;
      this.vat.percentage = this.vatForm.get('percentage')?.value;
      this.vat.description = this.vatForm.get('description')?.value;
      this.updated.emit(true);
      this.saveVAT();
    }
  }

  saveVAT() {
    if (this.vatForm?.valid) {
      this._vatService.updateVAT$(this.vat).subscribe();
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
            this._router.navigate(['/vat']);
          })
        }
      });

    })
  }


}
