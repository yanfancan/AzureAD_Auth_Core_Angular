import { Component, OnInit} from '@angular/core';
import { ShareService } from './_services/share.service';
import { AdalService } from 'adal-angular4';
import { Router } from '@angular/router';
import { environment } from '../environments/environment';
import { ConfigService } from 'ngx-envconfig';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'AVLAdminClient';
  public values: string[];
  logout = false;
  currentUser: string;
  isSignedIn: boolean;

  private adalConfig = environment.adalConfig;

  constructor(
    private adal: AdalService,
    private router: Router,
    private shareService: ShareService,
    private configService: ConfigService
  ) {

    console.log("in AppComponent ... " + this.configService.getApi("USER"));
    console.log("in AppComponent ... env= " + this.configService.getEnv());
    this.adal.init(this.adalConfig);
    this.shareService.UserInfo.subscribe(
      u => {
        if (u != undefined) {
          this.currentUser = u.userName;
          this.isSignedIn = u.authenticated;
          console.log("UserInfo: " + JSON.stringify(u) + " authenticated:" + u.authenticated);
        }
      })
  }

  ngOnInit() {
    this.shareService.refresh();
  }

  signoutButton() {

    this.adal.logOut();
    this.shareService.refresh();
    this.router.navigate(['/signedout']);
  }

  signinButton() {

    this.adal.login();
   
  }
}
