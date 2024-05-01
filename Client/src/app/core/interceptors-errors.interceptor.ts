import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


@Injectable()
export class InterceptorsErrorsInterceptor implements HttpInterceptor {

  constructor(private _Router: Router , private _ToastrService :ToastrService ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError( (err: HttpErrorResponse) => {
        if(err) {
          if(err.status === 400){
            if(err.error.errrors){
              throw err.error;
            }else{
             this._ToastrService.error(err.error.message , err.status.toString())

            }
          }
          if(err.status === 401){
            this._ToastrService.error(err.error.message , err.status.toString())
          }
          if(err.status === 400){
            this._Router.navigateByUrl('/not-found')
          };
          if(err.status === 500){
            this._Router.navigateByUrl('/server-error')
          }
        }
        return throwError(() => new Error(err.message) )
      })
      )
  }
}
