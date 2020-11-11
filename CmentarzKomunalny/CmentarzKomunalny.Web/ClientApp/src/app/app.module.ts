import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { AktualnosciComponent } from './aktualnosci/aktualnosci/aktualnosci.component';
import { StronaGlownaComponent } from './strona-glowna/strona-glowna/strona-glowna.component';
import { WyszukiwarkaGrobowComponent } from './wyszukiwarka-grobow/wyszukiwarka-grobow/wyszukiwarka-grobow.component';
import { NekrologiComponent } from './nekrologi/nekrologi/nekrologi.component';
import { InformacjeComponent } from './informacje/informacje/informacje.component';
import { KontaktComponent } from './kontakt/kontakt/kontakt.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AktualnosciComponent,
    StronaGlownaComponent,
    WyszukiwarkaGrobowComponent,
    NekrologiComponent,
    InformacjeComponent,
    KontaktComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MatIconModule,
    MatCardModule,
    MatDividerModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: StronaGlownaComponent, pathMatch: 'full' },
      { path: 'strona-glowna', component: StronaGlownaComponent, pathMatch: 'full' },
      { path: 'aktualnosci', component: AktualnosciComponent },
      { path: 'informacje', component: InformacjeComponent },
      { path: 'wyszukiwarka-grobow', component: WyszukiwarkaGrobowComponent },
      { path: 'nekrologi', component: NekrologiComponent },
      { path: 'kontakt', component: KontaktComponent },
    ]),
    BrowserAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
