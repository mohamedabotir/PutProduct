import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Product } from 'src/Shared/Products';

@Injectable({
  providedIn: 'root'
})
export class CartService {
public static Products:any;
private temp?:Product[];
  constructor(private http:HttpClient) {

   }
returnProducts(){
  console.log(CartService.Products);
  return CartService.Products;
}
  findProduct(id:any){
    let product;
    if(!CartService.Products)
    return null;
   for (let index = 0; index < CartService.length; index++) {
   if(CartService.Products[index].id == id)
   product= CartService.Products[index];
   }

   return product;
  }
  addProduct(product:Product){
    let result = this.findProduct(product.id) as unknown as Product;
   if(result){
     result.qty =result.qty+1;
     console.log(result.qty);
   this.modifyProduct(result.qty,result.id)
  }
   else{
     product.qty=1;
     CartService.Products.push(product);
   }
   console.log(CartService.Products);
  }
  modifyProduct(qty:any,id:any){
    CartService.Products.forEach((element: { id: any; qty: any; }) => {
      if(element.id == id){
        element.qty = qty;
        return;
      }
    });
  }
}
