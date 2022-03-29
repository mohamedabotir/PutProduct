import { ToastrService } from 'ngx-toastr';
import { Product } from './../../Shared/Products';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../Services/product.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { map, mergeMap } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductEditComponent implements OnInit {
id?:Number;

  constructor(private route:ActivatedRoute,private Product:ProductService,private fb:FormBuilder , private toast:ToastrService) { }
  form=this.fb.group({
    description: [],
    quantity: [],
    name: [],
    price: [],
    categoryId: [],
    id:[],
    userId:[],
    imageUrl: [],
  });
  CurrentProduct?:Product;
  ngOnInit(): void {
    this.onStart();
  }
  onStart(){

    this.route.params.pipe(map(param=>{
      this.id=param["id"];
      return this.id;
    }),mergeMap(id=>this.Product.getProduct(id))).subscribe(data=>{
      this.CurrentProduct = data;
      this.form= this.fb.group({
        description: [this.CurrentProduct?.description,],
        quantity: [this.CurrentProduct?.quantity,],
        name: [this.CurrentProduct?.name,],
        price: [this.CurrentProduct?.price,],
        categoryId: [this.CurrentProduct?.categoryId,],
        id:[this.CurrentProduct?.id,],
        userId:[this.CurrentProduct?.userId],
        imageUrl: [this.CurrentProduct?.imageUrl,],

        });

    });
  }
  onSubmit(){
this.Product.updateProduct(this.form.value).subscribe(data=>{
  this.toast.success("Product Updated Successfully");
  console.log(data);
})
  }

}
