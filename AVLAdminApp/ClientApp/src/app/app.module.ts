import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpBackend, HttpClient } from '@angular/common/http';
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

import { ConfigModule, ConfigService } from 'ngx-envconfig';
import { environment } from '../environments/environment';
import { ConfigFactory } from 'ngx-envconfig/src/config.module';


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
    NgxPaginationModule,
  
     
   ConfigModule.forRoot(environment),
  ],
  providers: [
    AuthGuard,
    AuthenticationService,
    CookieService,

    //this will override the default "ConfigService" provide in the imported "ConfigModule"
    //and inject the instance of ConfigService created from the below factory to the imported "ConfigModule"
    {
      provide: ConfigService, useFactory: (hBackend: HttpBackend) =>
      {
        let configService = new ConfigService(new HttpClient(hBackend));
        return configService;
      },
      deps: [HttpBackend], //important: we have to specify every dependancie in "deps" for every parameter in the factory function
    },

    //******************************
    //since there is already APP_INITIALIZER specified in the imported "ConfigModule" which calls "ConfigService.load(env)"
    //we do not need to define it again
    //*****************************
    //{
    //  provide: APP_INITIALIZER, useFactory: (configService: ConfigService) => () => {
    //    configService.load({ state: 'production' });
    //    console.log("custom load config service $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
        
    //  },
    //  deps: [ConfigService],
    //  multi: true
    //},

    { provide: HTTP_INTERCEPTORS, useClass: APIInterceptor, multi: true },
    AdalService, { provide: HTTP_INTERCEPTORS, useClass: AdalInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
