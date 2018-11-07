import { Component, OnInit, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { AdalService } from 'adal-angular4';
import { ShareService } from '../_services/share.service';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  //styleUrls: ['./auth-callback.component.css']
})
export class AuthCallbackComponent implements OnInit {

  constructor(private router: Router, private adal: AdalService, private _zone: NgZone, private shareService: ShareService) { }

  ngOnInit() {
    this.adal.handleWindowCallback();
    this.shareService.refresh();
    setTimeout(() => {
      this._zone.run(
        () => this.router.navigateByUrl("/", {skipLocationChange: false})
      );
    }, 200);
  }

}
