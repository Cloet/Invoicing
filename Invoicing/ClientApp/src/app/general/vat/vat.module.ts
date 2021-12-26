import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddVatComponent } from './add-vat/add-vat.component';
import { EditVatComponent } from './edit-vat/edit-vat.component';
import { VatDisplayScreenComponent } from './vat-display-screen/vat-display-screen.component';
import { VatTableComponent } from './vat-table/vat-table.component';
import { DeleteVatDialogComponent } from './delete-vat-dialog/delete-vat-dialog.component';
import { RouterModule, Routes } from '@angular/router';
import { VAT } from '../../common/model/vat.model';
import { SharedModule } from '../../shared/shared.module';
import { VATService } from '../../common/service/vat.service';

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
        path: 'add-vat',
        component: AddVatComponent,
        data: {
          breadcrumb: 'Create'
        }
      },
      {
        path: 'edit-vat/:id',
        component: EditVatComponent,
        data: {
          breadcrumb: 'Edit'
        }
      }
    ]
  }
]

@NgModule({
  declarations: [
    AddVatComponent,
    EditVatComponent,
    VatDisplayScreenComponent,
    VatTableComponent,
    DeleteVatDialogComponent
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
