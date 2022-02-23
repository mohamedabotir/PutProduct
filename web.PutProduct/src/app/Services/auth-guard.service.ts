import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private route:Router,private auth:AuthService) { }
  canActivate(): boolean {
   if(this.auth.isAuthenticated()){
     return true;
   }
   else{
     this.route.navigate(["login"]);
     return false;
   }
  }
}
