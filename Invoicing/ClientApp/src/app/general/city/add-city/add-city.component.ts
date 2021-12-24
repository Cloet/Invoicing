import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { City } from '../../../common/model/city.model';
import { Country } from '../../../common/model/country.model';
import { CityService } from '../../../common/service/city.service';
import { CountryService } from '../../../common/service/country.service';
import { ErrorDialogComponent } from '../../../error-dialog/error-dialog.component';

@Component({
  selector: 'app-add-city',
  templateUrl: './add-city.component.html',
  styleUrls: ['./add-city.component.scss']
})
export class AddCityComponent implements OnInit {

  @Input() city!: City;
  @Output() updated = new EventEmitter<boolean>();
  cityForm!: FormGroup;
  hideCountryList: boolean = false;

  constructor(
    private fb: FormBuilder,
    private _cityService: CityService,
    private _dialog: MatDialog,
    private router: Router) { }

  ngOnInit(): void {

    this._cityService.postError$.subscribe(
      error => {
        this.showErrorDialog(error);
      }
    );


    this.hideCountryList = false;
    this.cityForm = this.fb.group({
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(3)
        ]
      ],
      postal: [
        '',
        [
          Validators.required,
          Validators.minLength(3)
        ]
      ],
      countrycode: [
        '',
        [
          Validators.required,
          Validators.minLength(3)
        ]
      ],
      countryname: [
        '',
        [
          Validators.required,
          Validators.minLength(3)
        ]
      ]
    });
    this.cityForm.setValidators(this.validateCountry());

    this.city = new City();
    this.city.country = new Country();
    this.cityForm.controls.countrycode.disable();
    this.cityForm.controls.countryname.disable();
  }

  validateCountry() : ValidatorFn {

    let valid =  (group: AbstractControl): ValidationErrors | null => {
      if (this.city.country === null || (this.city.country != null && this.city.country?.id <= 0)) {
        return { required: true };
      }

      return null;
    };

    return valid;

  }

  onSubmit() {
    if (this.cityForm?.valid) {
      this.city.name = this.cityForm.get('name')?.value;
      this.city.postal = this.cityForm.get('postal')?.value;
      this.updated.emit(true);
      this.addCity();
    }
  }

  onSelect() {
    this.hideCountryList = true;
  }

  setCountry(country: Country) {
    this.city.country = country;

    this.cityForm.controls.countrycode.setValue(country.countryCode);
    this.cityForm.controls.countryname.setValue(country.name);
    this.hideCountryList = false;
  }

  addCity() {
    this._cityService.createCity$(this.city).subscribe(response => {
      if (response.id != null && response.id > 0) {
        this.router.navigate(['/city']);
      }
    });
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
