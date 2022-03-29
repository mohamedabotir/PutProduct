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
  constructor(private fb:FormBuilder,private Auth:AuthService) {
  }

  ngOnInit(): void {
  }
onsubmit():void{
this.Auth.Login(this.form.value).subscribe(data=>{
  this.Auth.saveToken(data)
});
console.log(this.form)
}
}
