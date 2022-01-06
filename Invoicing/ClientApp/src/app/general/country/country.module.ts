import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { RouterModule, Routes } from '@angular/router';
import { DeleteCountryDialogComponent } from './delete-country-dialog/delete-country-dialog.component';
import { CountryDisplayScreenComponent } from './country-display-screen/country-display-screen.component';
import { CountryService } from '../../common/service/country.service';
import { AddEditCountryComponent } from './add-edit-country/add-edit-country.component';
import { CountryViewComponent } from './country-view/country-view.component';

const countryRoutes: Routes = [
  {
    path: '',
    component: CountryDisplayScreenComponent,
    data: {
      breadcrumb: 'Country'
    },
    children: [
      {
        path: '',
        component: CountryViewComponent,
      },
      {
        path: 'add',
        component: AddEditCountryComponent,
        data: {
          breadcrumb: 'Create'
        }
      },
      {
        path: 'edit/:id',
        component: AddEditCountryComponent,
        data: {
          breadcrumb: 'Edit'
        }
      }
    ]
  }
]

@NgModule({
  declarations: [
    CountryViewComponent,
    DeleteCountryDialogComponent,
    CountryDisplayScreenComponent,
    AddEditCountryComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(countryRoutes)
  ],
  providers: [
    CountryService
  ]
})
export class CountryModule { }
