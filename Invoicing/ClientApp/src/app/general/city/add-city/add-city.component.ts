import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { BaseComponent } from '../../../common/base.component';
import { City } from '../../../common/model/city.model';
import { Country } from '../../../common/model/country.model';
import { BaseService } from '../../../common/service/base.service';
import { CityService } from '../../../common/service/city.service';

@Component({
  selector: 'app-add-city',
  templateUrl: './add-city.component.html',
  styleUrls: ['./add-city.component.scss']
})
export class AddCityComponent extends BaseComponent implements OnInit {

  @Input() city!: City;
  @Output() updated = new EventEmitter<boolean>();
  cityForm!: FormGroup;
  hideCountryList: boolean = true;

  constructor(
    private fb: FormBuilder,
    private _cityService: CityService,
    private _dialog: MatDialog,
    private _snackbar: MatSnackBar,
    private router: Router) {
    super(_dialog);
  }

  ngOnInit(): void {
    this.hideCountryList = true;
    this.subscribeToErrors<City>(this._cityService);

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
      mainmuncipality: [
        ''
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
      this.city.mainMunicipality = this.cityForm.get('mainmuncipality')?.value;
      this.updated.emit(true);
      this.addCity();
    }
  }

  onSelect() {
    this.hideCountryList = false;
  }

  setCountry(country: Country) {
    if (country != undefined) {
      this.city.country = country;
      this.cityForm.controls.countrycode.setValue(country.countryCode);
      this.cityForm.controls.countryname.setValue(country.name);
    }
    this.hideCountryList = true;
  }

  addCity() {
    this._cityService.createCity$(this.city).subscribe(response => {
      if (response.id != null && response.id > 0) {
        this.router.navigate(['/city']);
        this._snackbar.open("City has been created.", 'ok', { duration: 2000 });
      }
    });
  }

}
