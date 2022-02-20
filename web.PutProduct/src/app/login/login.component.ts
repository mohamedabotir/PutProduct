import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form:FormGroup;
  constructor(private fb:FormBuilder) {
this.form = fb.group({
  username:[''],
  password:['']
});

  }

  ngOnInit(): void {
  }
onsubmit():void{

console.log("submit",this.form.value);
}
}
