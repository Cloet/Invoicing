import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddEditAddressComponent } from './add-edit-address/add-edit-address.component';
import { AddressDisplayScreenComponent } from './address-display-screen/address-display-screen.component';
import { AddressTableComponent } from './address-table/address-table.component';
import { DeleteAddressDialogComponent } from './delete-address-dialog/delete-address-dialog.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { AddressService } from '../../common/service/address.service';

const addressRoutes: Routes = [
  {
    path: '',
    component: AddressDisplayScreenComponent,
    data: {
      breadcrumb: 'Address'
    },
    children: [
      {
        path: '',
        component: AddressTableComponent
      },
      {
        path: 'add',
        component: AddEditAddressComponent,
        data: {
          breadcrumb: 'Create'
        }
      },
      {
        path: 'edit/:id',
        component: AddEditAddressComponent,
        data: {
          breadcrumb: 'Edit'
        }
      }
    ]
  }
]

@NgModule({
  declarations: [
    AddEditAddressComponent,
    AddressDisplayScreenComponent,
    AddressTableComponent,
    DeleteAddressDialogComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(addressRoutes)
  ],
  providers: [
    AddressService
  ]
})
export class AddressModule { }
