import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
apiPath = environment.url;
  constructor(private push:HttpClient,private Auth:AuthService) {


  }
pushProduct(data:any):Observable<any>{

return this.push.post(this.apiPath+"product/create",data);
}
}
