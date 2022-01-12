import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '../../../common/base.component';
import { Address} from '../../../common/model/address.model';
import { City } from '../../../common/model/city.model';
import { AddressService } from '../../../common/service/address.service';
import { DeleteAddressDialogComponent } from '../delete-address-dialog/delete-address-dialog.component';

@Component({
  selector: 'app-add-edit-address',
  templateUrl: './add-edit-address.component.html',
  styleUrls: ['./add-edit-address.component.scss']
})
export class AddEditAddressComponent extends BaseComponent implements OnInit {

  @Input() address!: Address;
  addressForm!: FormGroup;
  hideCityList: boolean = true;
  id!: string;
  isAddMode!: boolean;

  constructor(
    private _fb: FormBuilder,
    private _addressService: AddressService,
    private _dialog: MatDialog,
    private _snackbar: MatSnackBar,
    private _router: Router,
    private _route: ActivatedRoute
  ) {
    super(_dialog);
  }

  private createFormGroup(formBuilder: FormBuilder): FormGroup {
    return formBuilder.group({
      address: formBuilder.group({
        name: [
          '',
          [
            Validators.required,
            Validators.minLength(3)
          ]
        ],
        number: [
          '',
          [
            Validators.required
          ]
        ],
        street: [
          '',
          [
            Validators.required
          ]
        ],
        city: formBuilder.group({
          ame: [
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
      })
    })
  }

  ngOnInit(): void {
    this.id = this._route.snapshot.params['id'];
    this.isAddMode = !this.id;

    this.hideCityList = true;
    this.subscribeToErrors<Address>(this._addressService);

    this.address = new Address();
    this.addressForm = this.createFormGroup(this._fb);

    if (!this.isAddMode) {
      this._addressService.getAddressForId$(Number(this.id)).subscribe(val => {
        this.address = val;
        this.addressForm.get('address')?.patchValue(val);
      })
    }

  }

  onSubmit() {
    if (this.addressForm?.valid) {
      this.address.updatePartial(this.addressForm.getRawValue().address);
      if (this.isAddMode) {
        this.addAddress();
      } else {
        this.updateAddress();
      }
    }
  }

  onSelect() {
    this.hideCityList = false;
  }

  setCity(city: City) {
    if (city != undefined) {
      this.address.city = city;
      this.addressForm.get('address.city')?.patchValue(city);
    }
    this.hideCityList = true;
  }

  addAddress() {
    this._addressService.createAddress$(this.address).subscribe(response => {
      if (response.id != null && response.id > 0) {
        this._router.navigate(['/address']);
        this._snackbar.open('Address has been created.', 'ok', { duration: 2000 });
      }
    });
  }

  updateAddress() {
    this._addressService.updateAddress$(this.address).subscribe(response => {
      if (response.id != null && response.id > 0) {
        this._snackbar.open("Address has been saved.", "ok", { duration: 2000 });
      }
    });
  }

  deleteAddress() {
    this._addressService.getAddressForId$(this.address.id).subscribe(val => {
      this.address = val;

      if (this.address.id === undefined || this.address.id <= 0) {
        this.showErrorDialog("Cannot delete city, entry not found.");
        this._router.navigate(['/address']);
        return;
      }

      const dialogConfig = new MatDialogConfig();
      dialogConfig.autoFocus = true;
      const dialogRef = this._dialog.open(DeleteAddressDialogComponent, dialogConfig);

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this._addressService.deleteAddress(this.address.id).subscribe(res => {
            this._snackbar.open("Address has been deleted.", "ok", { duration: 2000 });
            this._router.navigate(['/address']);
          });
        }
      });

    });
  }

}
