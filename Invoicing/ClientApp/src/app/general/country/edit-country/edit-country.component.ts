import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '../../../common/base.component';
import { Country } from '../../../common/model/country.model';
import { CountryService } from '../../../common/service/country.service';
import { ErrorDialogComponent } from '../../../error-dialog/error-dialog.component';
import { DeleteCountryDialogComponent } from '../delete-country-dialog/delete-country-dialog.component';


@Component({
  selector: 'app-edit-country',
  templateUrl: './edit-country.component.html',
  styleUrls: ['./edit-country.component.scss']
})
export class EditCountryComponent extends BaseComponent implements OnInit {

  @Input() country!: Country;
  @Output() updated = new EventEmitter<boolean>();
  countryForm!: FormGroup;

  constructor(private fb: FormBuilder
    , private _countryService: CountryService
    , private router: Router
    , private _route: ActivatedRoute
    , private _dialog: MatDialog,
    private _snackbar: MatSnackBar
  ) {
    super(_dialog);
  }

  ngOnInit(): void {
    this.subscribeToErrors<Country>(this._countryService);

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
    this._countryService.getCountryForId$(this.country.id).subscribe(val => {
      this.country = val;

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
            this._snackbar.open("Country has been deleted.", 'ok', { duration: 2000 });
            this.router.navigate(['/country']);
          });
        }
      });

    });

  }

  saveCountry() {
    if (this.countryForm?.valid) {
      this._countryService.updateCountry$(this.country).subscribe(response => {
        this._snackbar.open("Country has been saved", 'ok', { duration: 2000 });
      }
      );
    }
  }

}
