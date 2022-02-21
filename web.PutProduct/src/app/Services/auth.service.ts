import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUr =environment.url+'identity/';
  constructor(private auth:HttpClient) {

  }
  Login(data:any):Observable<any>{
    return this.auth.post(this.apiUr+"login",data);
  }

  Register(data:any):Observable<any>{
    return this.auth.post(this.apiUr+"register",data);
  }
  saveToken(token:string){
    localStorage.setItem('token',token);
  }
  get Token(){
    return localStorage.getItem('token');
  }
}
