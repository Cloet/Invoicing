import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '../../../common/base.component';
import { City } from '../../../common/model/city.model';
import { Country } from '../../../common/model/country.model';
import { CityService } from '../../../common/service/city.service';
import { DeleteCityDialogComponent } from '../delete-city-dialog/delete-city-dialog.component';

@Component({
  selector: 'app-add-edit-city',
  templateUrl: './add-edit-city.component.html',
  styleUrls: ['./add-edit-city.component.scss']
})
export class AddEditCityComponent extends BaseComponent implements OnInit {

  @Input() city!: City;
  cityForm!: FormGroup;
  hideCountryList: boolean = true;
  id!: string;
  isAddMode!: boolean;

  constructor(
    private _fb: FormBuilder,
    private _cityService: CityService,
    private _dialog: MatDialog,
    private _snackbar: MatSnackBar,
    private _router: Router,
    private _route: ActivatedRoute) {
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
          id: [
            {
              value: 0,
              disabled: true
            }
          ],
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
    this.id = this._route.snapshot.params['id'];
    this.isAddMode = !this.id

    this.hideCountryList = true;
    this.subscribeToErrors<City>(this._cityService);

    this.city = new City();
    this.city.country = new Country();
    this.cityForm = this.CreateFormGroup(this._fb);
    this.cityForm.setValidators(this.validateCountry());

    if (!this.isAddMode) {
      this._cityService.getCityForId$(Number(this.id)).subscribe(val => {
        this.city = val;
        this.cityForm.get("city")?.patchValue(val);
      });
    }
  }

  validateCountry(): ValidatorFn {

    let valid = (group: AbstractControl): ValidationErrors | null => {
      if (this.city.country === null || (this.city.country != null && this.city.country?.id <= 0)) {
        return { required: true };
      }

      return null;
    };

    return valid;

  }

  onSubmit() {
    if (this.cityForm?.valid) {
      this.city.updatePartial(this.cityForm.getRawValue().city);
      if (this.isAddMode) {
        this.addCity();
      } else {
        this.updateCity();
      }
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
        this._router.navigate(['/city']);
        this._snackbar.open("City has been created.", 'ok', { duration: 2000 });
      }
    });
  }

  updateCity() {
    this._cityService.updateCity$(this.city).subscribe(response => {
      if (response.id != null && response.id > 0) {
        this._snackbar.open("City has been saved.", "ok", { duration: 2000 });
      }
    })
  }

  deleteCity() {
    this._cityService.getCityForId$(this.city.id).subscribe(val => {
      this.city = val;

      if (this.city.id === undefined || this.city.id <= 0) {
        this.showErrorDialog("Cannot delete city, entry not found.");
        this._router.navigate(['/city']);
        return;
      }

      const dialogConfig = new MatDialogConfig();
      dialogConfig.autoFocus = true;
      const dialogRef = this._dialog.open(DeleteCityDialogComponent, dialogConfig);

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this._cityService.deleteCity(this.city.id).subscribe(res => {
            this._snackbar.open("City has been deleted.", 'ok', { duration: 2000 });
            this._router.navigate(['/city']);
          });
        }
      });

    });

  }

}
