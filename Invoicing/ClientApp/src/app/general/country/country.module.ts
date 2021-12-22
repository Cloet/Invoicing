import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { RouterModule, Routes } from '@angular/router';
import { AddCountryComponent } from './add-country/add-country.component';
import { DeleteCountryDialogComponent } from './delete-country-dialog/delete-country-dialog.component';
import { EditCountryComponent } from './edit-country/edit-country.component';
import { CountryViewComponent } from './country-view/country-view.component';
import { CountryDisplayScreenComponent } from './country-display-screen/country-display-screen.component';
import { CountryService } from '../../common/service/country.service';

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
        path: 'add-country',
        component: AddCountryComponent,
        data: {
          breadcrumb: 'Create'
        }
      },
      {
        path: 'edit-country/:id',
        component: EditCountryComponent,
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
    AddCountryComponent,
    DeleteCountryDialogComponent,
    EditCountryComponent,
    CountryDisplayScreenComponent
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
