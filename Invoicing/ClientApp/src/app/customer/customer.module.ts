import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddEditCustomerComponent } from './add-edit-customer/add-edit-customer.component';
import { CustomerDisplayScreenComponent } from './customer-display-screen/customer-display-screen.component';
import { CustomerTableComponent } from './customer-table/customer-table.component';
import { DeleteCustomerDialogComponent } from './delete-customer-dialog/delete-customer-dialog.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { CustomerService } from '../common/service/customer.service';

const customerRoutes: Routes = [
  {
    path: '',
    component: CustomerDisplayScreenComponent,
    data: {
      breadcrumb: 'Customer'
    },
    children: [
      {
        path: '',
        component: CustomerTableComponent
      },
      {
        path: 'add',
        component: AddEditCustomerComponent,
        data: {
          breadcrumb: 'Create'
        }
      },
      {
        path: 'edit/:id',
        component: AddEditCustomerComponent,
        data: {
          breadcrumb: 'Edit'
        }
      }
    ]
  }
]

@NgModule({
  declarations: [
    AddEditCustomerComponent,
    CustomerDisplayScreenComponent,
    CustomerTableComponent,
    DeleteCustomerDialogComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(customerRoutes)
  ],
  providers: [
    CustomerService
  ]
})
export class CustomerModule { }
