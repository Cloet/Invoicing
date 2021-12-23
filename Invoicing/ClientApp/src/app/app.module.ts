import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared/shared.module';
import { CoreModule } from './core/core.module';
import { LandingPageComponent } from './core/landing-page/landing-page.component';
import { PageNotFoundComponent } from './core/page-not-found/page-not-found.component';
import { ErrorDialogComponent } from './error-dialog/error-dialog.component';
import { BreadcrumbService } from 'xng-breadcrumb';

const appRoutes: Routes = [
  { path: '', component: LandingPageComponent },
  { path: 'country', loadChildren: () => import('./general/country/country.module').then(m => m.CountryModule) },
  { path: 'city', loadChildren: () => import('./general/city/city.module').then(m => m.CityModule) },
  { path: 'not-found', component: PageNotFoundComponent},
  { path: '**', component: PageNotFoundComponent}
]

@NgModule({
  declarations: [
    AppComponent,
    ErrorDialogComponent
  ],
  imports: [
    SharedModule,
    CoreModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule
  ],
  providers: [
    BreadcrumbService
  ],
  bootstrap: [
    AppComponent
  ],
  entryComponents: [
    ErrorDialogComponent
  ]
})
export class AppModule { }
