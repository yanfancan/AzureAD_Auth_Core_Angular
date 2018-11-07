import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';


@Injectable()
export class APIInterceptor implements HttpInterceptor {
  private apiURLRoot: string = environment.apiURLRoot;

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    console.log("APIInterceptor is processing url = "+req.url);
    if (req.url.startsWith("/api")) {
      const apiReq = req.clone({ url: `${this.apiURLRoot}${req.url}` });
      return next.handle(apiReq);
    } else {
      return next.handle(req);
    }
  }
}
