import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { Router } from '@angular/router';
import { Profile } from 'src/Shared/Profile';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUr =environment.url+'identity/';
  profile = environment.url+'profile/';
  userId?:String;
  public redirectUrl?: String;
  constructor(private auth:HttpClient,private route:Router) {

  }
  Login(data:any):Observable<any>{
    return this.auth.post<any>(this.apiUr+"login",data);
  }

  Register(data:any):Observable<any>{
    return this.auth.post(this.apiUr+"register",data);
  }
  saveToken(token:string){
    localStorage.setItem('token',token);
  }
   Token(){
    return localStorage.getItem('token');
  }
  isAuthenticated():boolean{
   if(this.Token())
   {return true;}
   return false;
  }
  getUserId(){
   return  this.auth.get(this.apiUr+"GetUserId");
  }
   getProfile(id:string):Observable<Profile>{
    return  this.auth.get<Profile>(this.profile + "index/" + id);
  }
  updateProfile(profile:Profile){
   return this.auth.post<Profile>(this.profile+"updateProfile",profile);
  }
}
