import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, retry, tap, throwError } from 'rxjs';
import { ToastrService } from "ngx-toastr"
@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptorService implements HttpInterceptor {

  constructor(private toast:ToastrService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let message = "";
   return next.handle(req).pipe(tap(evt=>{
    if (evt instanceof HttpResponse) {
      console.log(evt);
      if(evt.status==200 && evt.body["message"]){
          this.toast.success(evt.body["message"]);
      }
   }}),
    catchError((err)=>{


    if(err.status === 401)
    {
       message = "Please relogin and try again";
      this.toast.error(message);
    }
    else if(err.status === 404)
    {
      message = "NotFound";
     this.toast.error(message);
   }
    else if(err.status === 400)
    {
      message = "Can't Processed yout request";
     this.toast.error(message);
   }
    else
    {
      message = "Some thing gone wrong";
     this.toast.error(message);
   }

   return throwError(err);
  })
   );
  }
}
