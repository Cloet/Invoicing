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

const appRoutes: Routes = [
  { path: '', component: LandingPageComponent },
  { path: 'general', loadChildren: () => import('./general/general.module').then(m => m.GeneralModule)},
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
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [
    ErrorDialogComponent
  ]
})
export class AppModule { }
