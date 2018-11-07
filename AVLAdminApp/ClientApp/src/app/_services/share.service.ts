import { Injectable } from '@angular/core';
import { AdalService } from 'adal-angular4';
import { Observable, BehaviorSubject} from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class ShareService {

  private userInfo = new BehaviorSubject<adal.User>(undefined);

  constructor(private adal: AdalService) { }

  get UserInfo(): Observable<adal.User> {
    return this.userInfo.asObservable();
  }

  refresh() {
    this.userInfo.next(this.adal.userInfo);
  }

}
