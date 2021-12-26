import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { count } from 'rxjs/operators';
import { BaseComponent } from '../../../common/base.component';
import { City } from '../../../common/model/city.model';
import { Country } from '../../../common/model/country.model';
import { CityService } from '../../../common/service/city.service';
import { DeleteCityDialogComponent } from '../delete-city-dialog/delete-city-dialog.component';

@Component({
  selector: 'app-edit-city',
  templateUrl: './edit-city.component.html',
  styleUrls: ['./edit-city.component.scss']
})
export class EditCityComponent extends BaseComponent implements OnInit {

  @Input() city!: City;
  @Output() updated = new EventEmitter<boolean>();
  cityForm!: FormGroup;
  hideCountryList: boolean = false;

  constructor(
    private fb: FormBuilder,
    private _cityService: CityService,
    private _dialog: MatDialog,
    private router: Router,
    private _route: ActivatedRoute
  ) {
    super(_dialog);
  }

  ngOnInit(): void {
    this.subscribeToErrors<City>(this._cityService);

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
    //this.cityForm.controls.name.disable();
    this.cityForm.controls.postal.disable();
    this.cityForm.controls.countrycode.disable();
    this.cityForm.controls.countryname.disable();

    let id = this._route.snapshot.paramMap.get('id');
    if (id != null) {
      this._cityService.getCityForId$(Number(id)).subscribe(
        res => {
          this.city = res;
          this.cityForm.controls.name.setValue(res.name);
          this.cityForm.controls.postal.setValue(res.postal);
          this.cityForm.controls.countrycode.setValue(res.country?.countryCode);
          this.cityForm.controls.countryname.setValue(res.country?.name);
        }
      );
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
      this.city.name = this.cityForm.get('name')?.value;
      this.city.postal = this.cityForm.get('postal')?.value;
      this.updated.emit(true);
      this.updateCity();
    }
  }

  updateCity() {
    this._cityService.updateCity$(this.city).subscribe(
      response => {
        if (response.id != null && response.id > 0) {
          this.router.navigate(['/city']);
        }
      }
    )
  }

  onSelect() {
    this.hideCountryList = true;
  }

  setCountry(country: Country) {
    if (country != undefined) {
      this.city.country = country;
      this.cityForm.controls.countrycode.setValue(country.countryCode);
      this.cityForm.controls.countryname.setValue(country.name);
    }
    this.hideCountryList = false;
  }

  deleteCity() {
    this._cityService.getCityForId$(this.city.id).subscribe(
      val => {

        this.city = val;

        if (this.city.id === undefined || this.city.id <= 0) {
          this.showErrorDialog("Cannot delete city, entry not found.");
          this.router.navigate(['/city']);
          return;
        }

        const dialogConfig = new MatDialogConfig();
        dialogConfig.autoFocus = true;
        const dialogRef = this._dialog.open(DeleteCityDialogComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
          result => {
            if (result) {
              this._cityService.deleteCity(val.id).subscribe();
              this.router.navigate(['/city']);
            }
          }
        )

      }
    );
  }

}
