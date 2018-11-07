import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../_services/authentification.service';
import { NgForm } from '@angular/forms';
import { formModel } from '../form-model';
import { CookieService } from 'ngx-cookie-service';



@Component({
  styleUrls: ['./login.component.css'],
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  model = new formModel("", "");
  loading = false;
  returnUrl: string;
  failedAuth = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private cookieService: CookieService) { }

  ngOnInit() {

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  login() {
    this.loading = true;
    this.authenticationService.login(this.model.username, this.model.password)
      .subscribe(
      data => {
          this.cookieService.set('loggedIn', 'true');
          this.router.navigate([this.returnUrl]);
          location.reload();

        },
      error => {
          this.failedAuth = true;
          this.loading = false;
        });
    console.log(this.model.username)
    console.log(this.model.password)
  }

}
