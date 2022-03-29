import { CartService } from './../Services/cart-service.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../Services/auth.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form=this.fb.group({
    'Email':['',Validators.required],
    'Password':['',Validators.required]
  });;
  constructor(private fb:FormBuilder,private Auth:AuthService,private redirect:Router,private cartService:CartService) {

  }

  ngOnInit(): void {
    let userId;
    this.Auth.getUserId().subscribe(data=>{
    userId = data;
    });
    console.log(userId);
  if(userId)
  this.redirect.navigate(["/profile/"+userId]);
  }
onsubmit():void{
this.Auth.Login(this.form.value).subscribe(data=>{
  this.Auth.saveToken(data);
  this.redirect.navigate([""]);
  console.log(this.Auth.redirectUrl);
  if(this.Auth.redirectUrl){

    this.redirect.navigate([this.Auth.redirectUrl]);
    console.log(this.Auth.redirectUrl);
  }

});
console.log(this.form)
}
}
