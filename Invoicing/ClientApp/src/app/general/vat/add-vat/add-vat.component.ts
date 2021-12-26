import { EventEmitter, Output } from '@angular/core';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { BaseComponent } from '../../../common/base.component';
import { VAT } from '../../../common/model/vat.model';
import { VATService } from '../../../common/service/vat.service';

@Component({
  selector: 'app-add-vat',
  templateUrl: './add-vat.component.html',
  styleUrls: ['./add-vat.component.scss']
})
export class AddVatComponent extends BaseComponent implements OnInit {

  @Input() vat!: VAT;
  @Output() updated = new EventEmitter<boolean>();
  vatForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private _vatService: VATService,
    private _router: Router,
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
  }

  onSubmit() {
    if (this.vatForm?.valid) {
      this.vat.code = this.vatForm.get('code')?.value;
      this.vat.percentage = this.vatForm.get('percentage')?.value;
      this.vat.description = this.vatForm.get('description')?.value;
      this.updated.emit(true);
      this.addVAT();
    }
  }

  addVAT() {
    if (this.vatForm?.valid) {
      this._vatService.createVAT$(this.vat).subscribe(
        response => {
          if (response.id != null && response.id > 0) {
            this._router.navigate(['/vat']);
          }
        }
      );
    }
  }

}
