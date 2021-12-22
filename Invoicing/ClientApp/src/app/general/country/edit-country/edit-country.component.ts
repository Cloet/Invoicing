import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Country } from '../../../common/model/country.model';
import { CountryService } from '../../../common/service/country.service';
import { ErrorDialogComponent } from '../../../error-dialog/error-dialog.component';
import { DeleteCountryDialogComponent } from '../delete-country-dialog/delete-country-dialog.component';


@Component({
  selector: 'app-edit-country',
  templateUrl: './edit-country.component.html',
  styleUrls: ['./edit-country.component.scss']
})
export class EditCountryComponent implements OnInit {

  @Input() country!: Country;
  @Output() updated = new EventEmitter<boolean>();
  countryForm!: FormGroup;

  constructor(private fb: FormBuilder
    , private _countryService: CountryService
    , private router: Router
    , private _route: ActivatedRoute
    , private _dialog: MatDialog) { }

  ngOnInit(): void {
    this.countryForm = this.fb.group({
      code: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.pattern('[A-Z _-]*'),
        ],
      ],
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.pattern('[a-zA-Z0-9 _-]*')
        ]
      ]
    });
    this.countryForm.controls.code.disable();


    let id = this._route.snapshot.paramMap.get('id');

    if (id != null) {
      this._countryService.getCountryForId$(Number(id)).subscribe(res => {
        this.country = res;
        this.countryForm.controls.code.setValue(res.countryCode);
        this.countryForm.controls.name.setValue(res.name);
      });
    }
  }

  onSubmit() {
    if (this.countryForm?.valid) {
      this.country.name = this.countryForm.get('name')?.value;
      this.updated.emit(true);
      this.saveCountry();
    }
  }

  deleteCountry() {
    this._countryService.getCountryForId$(this.country.id).subscribe(val => { this.country = val; });

    if (this.country.id === undefined || this.country.id <= 0) {
      this.showErrorDialog("Cannot delete country, entry not found.");
      this.router.navigate(['/country']);
      return;
    }

    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    const dialogRef = this._dialog.open(DeleteCountryDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._countryService.deleteCountry(this.country.id).subscribe(res => {
          this.router.navigate(['/country']);
        });
      }
    });
  }

  saveCountry() {
    if (this.countryForm?.valid) {
      this._countryService.updateCountry$(this.country).subscribe(response => {
          
      }
      );
    }
  }

  showErrorDialog(errors: string) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      title: 'Error',
      errors
    };
    const dialogRef = this._dialog.open(ErrorDialogComponent, dialogConfig);

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

}
