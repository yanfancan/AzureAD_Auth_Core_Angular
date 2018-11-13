import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { ValuesComponent } from './values/values.component';
import { AppComponent } from './app.component';
import { AuthGuard } from './_guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { AuthCallbackComponent } from './auth-callback/auth-callback.component';
import { SignedoutComponent } from './signedout/signedout.component';
import { UnitsComponent } from './units/units.component';
import { ServiceAccountsComponent } from './service-accounts/service-accounts.component';


const routes: Routes = [
  //{ path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'values', component: ValuesComponent, canActivate: [AuthGuard] },
  { path: 'auth-callback', component: AuthCallbackComponent },
  { path: 'signedout', component: SignedoutComponent },
  { path: 'units', component: UnitsComponent, canActivate: [AuthGuard] },
  { path: 'service-accounts', component: ServiceAccountsComponent },
  //{ path: 'register', component: RegisterComponent },

  // otherwise redirect to home
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
