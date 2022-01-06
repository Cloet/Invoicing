import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VatDisplayScreenComponent } from './vat-display-screen/vat-display-screen.component';
import { VatTableComponent } from './vat-table/vat-table.component';
import { DeleteVatDialogComponent } from './delete-vat-dialog/delete-vat-dialog.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { VATService } from '../../common/service/vat.service';
import { AddEditVatComponent } from './add-edit-vat/add-edit-vat.component';

const vatRoutes: Routes = [
  {
    path: '',
    component: VatDisplayScreenComponent,
    data: {
      breadcrumb: 'VAT'
    },
    children: [
      {
        path: '',
        component: VatTableComponent,
        data: {
          breadcrumb: 'VAT'
        }
      },
      {
        path: 'add',
        component: AddEditVatComponent,
        data: {
          breadcrumb: 'Create'
        }
      },
      {
        path: 'edit/:id',
        component: AddEditVatComponent,
        data: {
          breadcrumb: 'Edit'
        }
      }
    ]
  }
]

@NgModule({
  declarations: [
    VatDisplayScreenComponent,
    VatTableComponent,
    DeleteVatDialogComponent,
    AddEditVatComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(vatRoutes)
  ],
  providers: [
    VATService
  ]
})
export class VatModule { }
