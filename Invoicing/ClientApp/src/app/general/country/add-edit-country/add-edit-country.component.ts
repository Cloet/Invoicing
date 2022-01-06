import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '../../../common/base.component';
import { Country } from '../../../common/model/country.model';
import { CountryService } from '../../../common/service/country.service';
import { DeleteCountryDialogComponent } from '../delete-country-dialog/delete-country-dialog.component';

@Component({
  selector: 'app-add-edit-country',
  templateUrl: './add-edit-country.component.html',
  styleUrls: ['./add-edit-country.component.css']
})
export class AddEditCountryComponent extends BaseComponent implements OnInit {

  @Input() country!: Country;
  countryForm!: FormGroup;
  id!: string;
  isAddMode!: boolean;

  constructor(
    private fb: FormBuilder,
    private _countryService: CountryService,
    private _router: Router,
    private _route: ActivatedRoute,
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
    this.id = this._route.snapshot.params['id'];
    this.isAddMode = !this.id

    this.subscribeToErrors<Country>(this._countryService);

    this.countryForm = this.CreateFormGroup(this.fb);
    this.country = new Country();

    if (!this.isAddMode) {
      this._countryService.getCountryForId$(Number(this.id)).subscribe(val => {
        this.country = val;
        this.countryForm.get('country')?.patchValue(val);
      });
    }

  }

  onSubmit() {
    if (this.countryForm?.valid) {
      this.country.updatePartial(this.countryForm.getRawValue().country);
      if (this.isAddMode) {
        this.addCountry();
      } else {
        this.updateCountry();
      }
    }
  }

  deleteCountry() {
    this._countryService.getCountryForId$(this.country.id).subscribe(val => {
      this.country = val;

      if (this.country.id === undefined || this.country.id <= 0) {
        this.showErrorDialog("Cannot delete country, entry not found.");
        this._router.navigate(['/country']);
        return;
      }

      const dialogConfig = new MatDialogConfig();
      dialogConfig.autoFocus = true;
      const dialogRef = this._dialog.open(DeleteCountryDialogComponent, dialogConfig);

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this._countryService.deleteCountry(this.country.id).subscribe(res => {
            this._snackbar.open("Country has been deleted.", 'ok', { duration: 2000 });
            this._router.navigate(['/country']);
          });
        }
      });

    });

  }

  updateCountry() {
    if (this.countryForm?.valid) {
      this._countryService.updateCountry$(this.country).subscribe(response => {
        if (response.id != null && response.id > 0) {
          this._snackbar.open('Country has been saved.', 'ok', { duration: 2000 });
        }
      });
    }
  }

  addCountry() {
    if (this.countryForm?.valid) {
      this._countryService.createCountry$(this.country).subscribe(response => {
        if (response.id != null && response.id > 0) {
          this._snackbar.open("Country has been created", 'ok', { duration: 2000 });
          this._router.navigate(['/country']);
        }
      });
    }
  }

}
