import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { BaseComponent } from '../../../common/base.component';
import { City } from '../../../common/model/city.model';
import { Country } from '../../../common/model/country.model';
import { CityService } from '../../../common/service/city.service';

@Component({
  selector: 'app-add-city',
  templateUrl: './add-city.component.html',
  styleUrls: ['./add-city.component.scss']
})
export class AddCityComponent extends BaseComponent implements OnInit {

  @Input() city!: City;
  cityForm!: FormGroup;
  hideCountryList: boolean = true;


  constructor(
    private _fb: FormBuilder,
    private _cityService: CityService,
    private _dialog: MatDialog,
    private _snackbar: MatSnackBar,
    private router: Router) {
    super(_dialog);
  }

  private CreateFormGroup(formBuilder: FormBuilder): FormGroup {
    return formBuilder.group({
      city: formBuilder.group({
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
        mainMunicipality: [
          false
        ],
        country: formBuilder.group({
          name: [
            {
              value: '',
              disabled: true
            },
            [
              Validators.required,
              Validators.minLength(3),
              Validators.pattern('[a-zA-Z0-9 _-]*')
            ]
          ],
          countryCode: [
            {
              value: '',
              disabled: true
            },
            [
              Validators.required,
              Validators.minLength(2),
              Validators.pattern('[A-Z _-]*')
            ]
          ]
        })
      })
    });
  }

  ngOnInit(): void {
    this.hideCountryList = true;
    this.subscribeToErrors<City>(this._cityService);

    this.city = new City();
    this.city.country = new Country();
    this.cityForm = this.CreateFormGroup(this._fb);
    this.cityForm.setValidators(this.validateCountry());
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
      this.city.updatePartial(this.cityForm.get('city')?.value);  
      this.addCity();
    }
  }

  onSelect() {
    this.hideCountryList = false;
  }

  setCountry(country: Country) {
    if (country != undefined) {
      this.city.country = country;
      this.cityForm.get('city.country')?.patchValue(country);
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
