import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CityDisplayScreenComponent } from './city-display-screen/city-display-screen.component';
import { AddCityComponent } from './add-city/add-city.component';
import { DeleteCityDialogComponent } from './delete-city-dialog/delete-city-dialog.component';
import { CityTableComponent } from './city-table/city-table.component';
import { EditCityComponent } from './edit-city/edit-city.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { CityService } from '../../common/service/city.service';

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
        component: CityTableComponent,
      },
      {
        path: 'add-city',
        component: AddCityComponent,
        data: {
          breadcrumb: 'Create'
        }
      },
      {
        path: 'edit-city/:id',
        component: EditCityComponent,
        data: {
          breadcrumb: 'edit'
        }
      }
    ]
  }
]

@NgModule({
  declarations: [
    CityDisplayScreenComponent,
    AddCityComponent,
    DeleteCityDialogComponent,
    CityTableComponent,
    EditCityComponent
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
