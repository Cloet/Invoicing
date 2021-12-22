import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViewCountryComponent } from './view-country/view-country.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { ViewCityComponent } from './view-city/view-city.component';
import { AddCountryComponent } from './add-country/add-country.component';

const generalRoutes: Routes = [
  {
    path: 'country',
    component: ViewCountryComponent
  },
  {
    path: 'add-country',
    component: AddCountryComponent
  },
  {
    path: 'city',
    component: ViewCityComponent
  }
];

@NgModule({
  declarations: [
    ViewCountryComponent,
    ViewCityComponent,
    AddCountryComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(generalRoutes),
  ]
})
export class GeneralModule { }
