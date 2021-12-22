import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Country } from '../../../common/model/country.model';
import { CountryService } from '../../../common/service/country.service';

@Component({
  selector: 'app-add-country',
  templateUrl: './add-country.component.html',
  styleUrls: ['./add-country.component.scss']
})
export class AddCountryComponent implements OnInit {

  @Input() country!: Country;
  @Output() updated = new EventEmitter<boolean>();
  countryForm!: FormGroup;

  constructor(private fb: FormBuilder, private _countryService: CountryService, private router: Router) {
    
  }

  ngOnInit(): void {
    this.countryForm = this.fb.group({
      code: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.pattern('[a-zA-Z]*')
        ]
      ],
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.pattern('[a-zA-Z0-9 _-]*')
        ]
      ]
    });
    this.country = new Country();
  }

  onSubmit() {
    if (this.countryForm?.valid) {
      this.country.countryCode = this.countryForm.get('code')?.value;
      this.country.name = this.countryForm.get('name')?.value;
      this.updated.emit(true);
      this.addCountry();
    }
  }

  addCountry() {
    if (this.countryForm?.valid) {
      this._countryService.createCountry$(this.country).subscribe(response => {
        //log('returned id' + response.id);
        this.router.navigate(
          ['/general/country']
        );
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
    const errors = {
      required: 'Required',
      minlength: `Min. amount of characters ${errorvalue.requiredLength}`,
      pattern: 'Only letters, digits, spaces, "_" and "-" are allowed'
    };
    return errors.required;
  }

}
