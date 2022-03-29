import { FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ProductService } from '../Services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
 productForm = this.fb.group({
  "description": ['',Validators.required],
  "quantity": ['',Validators.required],
  "name": ['',Validators.required],
  "price": ['',Validators.required],
  "categoryId": ['',Validators.required],
  "imageUrl": ['',Validators.required]

 });
  constructor(private product:ProductService,private fb:FormBuilder) {

  }

  ngOnInit(): void {
  }
  submitProduct():void{
  this.product.pushProduct(this.productForm.value).subscribe(data=>{
    console.log(data);
  });
  console.log(this.productForm.value);
  }

}
