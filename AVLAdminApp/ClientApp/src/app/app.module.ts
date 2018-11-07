import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { UsersComponent } from './users/users.component';
import { AuthGuard } from './_guards/auth.guard';
import { AuthenticationService } from './_services/authentification.service';
import { AppRoutingModule } from './/app-routing.module';
import { FormsModule } from '@angular/forms';
import { ValuesComponent } from './values/values.component';
import { HomeComponent } from './home/home.component';
import { CookieService } from 'ngx-cookie-service';
import { ServiceAccountsComponent } from './service-accounts/service-accounts.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import {NgxPaginationModule} from 'ngx-pagination';
import { UnitsComponent } from './units/units.component';
import { MapServicesComponent } from './map-services/map-services.component';
import { AuthCallbackComponent } from './auth-callback/auth-callback.component';
import { AdalService, AdalInterceptor } from 'adal-angular4';
import { SignedoutComponent } from './signedout/signedout.component';
import { APIInterceptor } from './http-client/APIIntercepter';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UsersComponent,
    ValuesComponent,
    HomeComponent,
    ServiceAccountsComponent,
    UnitsComponent,
    MapServicesComponent,
    AuthCallbackComponent,
    SignedoutComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    NgbModule,
    AppRoutingModule,
    FormsModule,
    Ng2SearchPipeModule,
    NgxPaginationModule
  ],
  providers: [
    AuthGuard,
    AuthenticationService,
    CookieService,
    
    { provide: HTTP_INTERCEPTORS, useClass: APIInterceptor, multi: true },
    AdalService, { provide: HTTP_INTERCEPTORS, useClass: AdalInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
