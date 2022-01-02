import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { BaseComponent } from '../../../common/base.component';
import { Country } from '../../../common/model/country.model';
import { CountryService } from '../../../common/service/country.service';

@Component({
  selector: 'app-add-country',
  templateUrl: './add-country.component.html',
  styleUrls: ['./add-country.component.scss']
})
export class AddCountryComponent extends BaseComponent implements OnInit {

  @Input() country!: Country;
  countryForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private _countryService: CountryService,
    private router: Router,
    private _dialog: MatDialog,
    private _snackbar: MatSnackBar
    ) {
    super(_dialog)
  }

  private CreateFormGroup(formBuilder: FormBuilder): FormGroup {
    return formBuilder.group({
      country: formBuilder.group({
        name: [
          '',
          [
            Validators.required,
            Validators.minLength(3),
            Validators.pattern('[a-zA-Z0-9 _-]*')
          ]
        ],
        countryCode: [
          '',
          [
            Validators.required,
            Validators.minLength(2),
            Validators.pattern('[A-Z _-]*')
          ]
        ]
      })
    });
  }

  ngOnInit(): void {
    this.subscribeToErrors<Country>(this._countryService);

    this.countryForm = this.CreateFormGroup(this.fb);
    this.country = new Country();
  }

  onSubmit() {
    if (this.countryForm?.valid) {
      const result: any = Object.assign({}, this.countryForm.value);
      this.country = result.country;
      this.addCountry();
    }
  }

  addCountry() {
    if (this.countryForm?.valid) {
      this._countryService.createCountry$(this.country).subscribe(response => {
        if (response.id != null && response.id > 0) {
          this._snackbar.open("Country has been created", 'ok', { duration: 2000 });
          this.router.navigate(['/country']);
        }
      });
    }
  }

  

}
