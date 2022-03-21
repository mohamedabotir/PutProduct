import { ToastrService } from 'ngx-toastr';
import { CartService } from './../Services/cart-service.service';
import { AuthService } from './../Services/auth.service';
import { ProductService } from './../Services/product.service';
import { Component, OnInit } from '@angular/core';
import { Product } from 'src/Shared/Products';

@Component({
  selector: 'app-list-products',
  templateUrl: './list-products.component.html',
  styleUrls: ['./list-products.component.css']
})
export class ListProductsComponent implements OnInit {
 Products?:Array<Product>;
 Temp?:Product;
 userId?:any;
  constructor(private Product:ProductService,private auth:AuthService,private cart:CartService,private tos:ToastrService) { }

  ngOnInit(): void {
this.getProducts();
this.auth.getUserId().subscribe(data=>{
this.userId = data;
});
  }
  getProducts():void{
    this.Product.getProducts().subscribe(data=>{
      this.Products = data;
      console.log(data);
    });
  }
  onDelete(id:Number):void{
this.Product.deleteProduct(id).subscribe(data=>{
  console.log(data);
  this.getProducts();
});
  }
  track(index:Number,Product:Product){
    return Product.id;
  }
  addToCart(product:Product){
   this.cart.addProduct(product);
  }

}
