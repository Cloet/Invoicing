import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { City } from '../../../common/model/city.model';
import { CityService } from '../../../common/service/city.service';
import { CountryService } from '../../../common/service/country.service';

@Component({
  selector: 'app-add-city',
  templateUrl: './add-city.component.html',
  styleUrls: ['./add-city.component.scss']
})
export class AddCityComponent implements OnInit {

  @Input() city!: City;
  @Output() updated = new EventEmitter<boolean>();
  cityForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private _cityService: CityService,
    private _countryService: CountryService,
    private router: Router) { }

  ngOnInit(): void {
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
      ]
    });
    this.city = new City();
  }

  onSubmit() {
    if (this.cityForm?.valid) {
      this.city.name = this.cityForm.get('name')?.value;
      this.city.postal = this.cityForm.get('postal')?.value;
      this.updated.emit(true);
      this._countryService.getCountryForId$(1).subscribe(
        resp => {
          this.city.country = resp;
          this.addCity();
        }
      );
    }
  }

  addCity() {
    if (this.cityForm?.valid) {
      this._cityService.createCity$(this.city).subscribe(response => {
        if (response.id != null && response.id > 0) {
          this.router.navigate(['/city']);
        }
      });
    }
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
