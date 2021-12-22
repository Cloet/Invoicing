import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { HeaderComponent } from './header/header.component';


@NgModule({
  declarations: [
    LandingPageComponent,
    PageNotFoundComponent,
    HeaderComponent
  ],
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    SharedModule,
    RouterModule.forChild([])
  ],
  exports: [
    PageNotFoundComponent,
    HeaderComponent
  ]
})

export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error('CoreModule has already been loaded.');
    }
  }
}
