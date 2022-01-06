import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { CityService } from '../../common/service/city.service';
import { CityTableComponent } from './city-table/city-table.component';
import { DeleteCityDialogComponent } from './delete-city-dialog/delete-city-dialog.component';
import { CityDisplayScreenComponent } from './city-display-screen/city-display-screen.component';
import { AddEditCityComponent } from './add-edit-city/add-edit-city.component';


const cityRoutes: Routes = [
  {
    path: '',
    component: CityDisplayScreenComponent,
    data: {
      breadcrumb: 'City'
    },
    children: [
      {
        path: '',
        component: CityTableComponent
      },
      {
        path: 'add',
        component: AddEditCityComponent,
        data: {
          breadcrumb: 'Create'
        }
      },
      {
        path: 'edit/:id',
        component: AddEditCityComponent,
        data: {
          breadcrumb: 'Edit'
        }
      }
    ]
  }
]

@NgModule({
  declarations: [
    CityDisplayScreenComponent,
    CityTableComponent,
    DeleteCityDialogComponent,
    AddEditCityComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(cityRoutes)
  ],
  providers: [
    CityService
  ]
})
export class CityModule { }
