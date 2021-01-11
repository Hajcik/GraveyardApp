import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { AktualnosciComponent } from './aktualnosci/aktualnosci/aktualnosci.component';
import { StronaGlownaComponent } from './strona-glowna/strona-glowna/strona-glowna.component';
import { WyszukiwarkaGrobowComponent } from './wyszukiwarka-grobow/wyszukiwarka-grobow/wyszukiwarka-grobow.component';
import { NekrologiComponent } from './nekrologi/nekrologi/nekrologi.component';
import { InformacjeComponent } from './informacje/informacje/informacje.component';
import { KontaktComponent } from './kontakt/kontakt/kontakt.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatDividerModule } from '@angular/material/divider';
import { MatStepperModule } from '@angular/material/stepper';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MapaComponent } from './mapa/mapa/mapa.component';
import { SharedService } from './shared.service';
import { RoczniceComponent } from './rocznice/rocznice/rocznice.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { WorkerPanelComponent } from './worker-panel/worker-panel.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatMomentDateModule } from '@angular/material-moment-adapter'
import { MatNativeDateModule } from '@angular/material'
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AktualnosciComponent,
    StronaGlownaComponent,
    WyszukiwarkaGrobowComponent,
    NekrologiComponent,
    InformacjeComponent,
    KontaktComponent,
    MapaComponent,
    RoczniceComponent,
    AdminPanelComponent,
    WorkerPanelComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MatIconModule,
    MatCardModule,
    MatSelectModule,
    MatDividerModule,
    MatStepperModule,
    MatFormFieldModule,
    MatExpansionModule,
    MatInputModule,
    MatButtonModule,            // <----- this module will be deprecated in the future version.
    MatDatepickerModule,        // <----- import(must)
    MatNativeDateModule,        // <----- import for date formating(optional)
    MatMomentDateModule,
    MatTableModule,
    MatCheckboxModule,
    MatPaginatorModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: StronaGlownaComponent, pathMatch: 'full' },
      { path: 'strona-glowna', component: StronaGlownaComponent, pathMatch: 'full' },
      { path: 'aktualnosci', component: AktualnosciComponent },
      { path: 'informacje', component: InformacjeComponent },
      { path: 'wyszukiwarka-grobow', component: WyszukiwarkaGrobowComponent },
      { path: 'nekrologi', component: NekrologiComponent },
      { path: 'mapa', component: MapaComponent },
      { path: 'kontakt', component: KontaktComponent },
      { path: 'rocznice', component: RoczniceComponent },
      { path: 'panelpracownika', component: WorkerPanelComponent },
      { path: 'paneladministratora', component: AdminPanelComponent },
    ]),
    BrowserAnimationsModule
  ],
  // providers: [
  //   { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
  //   SharedService
  // ],
  bootstrap: [AppComponent]
})
export class AppModule { }
