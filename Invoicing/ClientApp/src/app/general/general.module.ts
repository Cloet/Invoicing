import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { AddCountryComponent } from './country-view/add-country/add-country.component';
import { CountryViewComponent } from './country-view/country-view.component';
import { CityViewComponent } from './city-view/city-view.component';
import { DeleteCountryDialogComponent } from './country-view/delete-country-dialog/delete-country-dialog.component';
import { EditCountryComponent } from './country-view/edit-country/edit-country.component';

const generalRoutes: Routes = [
  {
    path: 'country',
    component: CountryViewComponent
  },
  {
    path: 'add-country',
    component: AddCountryComponent
  },
  {
    path: 'edit-country/:id',
    component: EditCountryComponent
  },
  {
    path: 'city',
    component: CityViewComponent
  }
];

@NgModule({
  declarations: [
    CountryViewComponent,
    CityViewComponent,
    AddCountryComponent,
    DeleteCountryDialogComponent,
    EditCountryComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(generalRoutes),
  ]
})
export class GeneralModule { }
