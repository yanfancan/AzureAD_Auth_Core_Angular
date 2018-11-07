import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { CookieService } from 'ngx-cookie-service';


@Injectable()
export class AuthenticationService {
  isLogedin: boolean = false;

  constructor(private http: HttpClient, private cookieService: CookieService) { }

  login(username: string, password: string) {
    console.log("sign in AAD invoked app component");
    return this.http.post<any>('/api/users/login', { username: username, password: password }).pipe(
      map(user => {
        if (user) {
          // store user details in local storage to keep user info
          localStorage.setItem('currentUser', JSON.stringify(user));
        }

        return user;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    this.cookieService.delete('loggedIn');
  }

  signin() {
    console.log("2nd sign in AAD invoked app component");
    return this.http.get<any>('/api/users/signin').pipe(
      map(isAuthenticated => {
        this.isLogedin = isAuthenticated;
      }))
  };


}
