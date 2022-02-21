import { AuthService } from './../Services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { groupBy } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
form = this.fb.group({
'Username':['',[Validators.required]],
'Phone':['',[Validators.required]],
'Email':['',[Validators.required,Validators.email]],
'Password':['',[Validators.required,Validators.pattern("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\]).{8,32}$")]]


});
  constructor(private fb:FormBuilder,private auth:AuthService) { }

  ngOnInit(): void {
  }
onSubmit(){
  this.auth.Register(this.form.value).subscribe(data=>{

    console.log(data);
  });
  console.log(this.form);
}
}
