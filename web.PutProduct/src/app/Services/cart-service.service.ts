import { ToastrService } from 'ngx-toastr';
import { Injectable, Output,EventEmitter } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Product } from 'src/Shared/Products';
let Cart:BehaviorSubject<Product>=new BehaviorSubject<Product>({id:0,description:'',name:'',price:0,categoryId:0,imageUrl:'',userId:'',userName:'',qty:0,quantity:0});

@Injectable()
export class  CartService {
  data:Product[]=[];
  constructor(private toast:ToastrService) {
  }
cart:Observable<Product[]>=new Observable<Product[]>();
returnProducts(){
  return JSON.parse(localStorage.getItem("products")!);
}

Delete(item:Product){
this.data =JSON.parse(localStorage.getItem("products")!);
for(let i=0 ;i<this.data.length;i++){
  if(this.data[i].id==item.id){
    this.data.splice(i,1);
  }
}
console.log(this.data);
localStorage.setItem("products",JSON.stringify(this.data));
}

  AddToCart(product:Product){
    Cart.next(product);
   let isFound = false
   if(product.quantity<1){
    this.toast.error("OutOfStock");
   }
    if(localStorage.getItem("products")){
      this.data = JSON.parse(localStorage.getItem("products")!);
      this.data.forEach(data=>{

        if(data.id == product.id){
           isFound=true;
          if(data.quantity < data.qty+1){
            this.toast.error("Exceed Limit");
            return;
          }
          data.qty = data.qty+1;
          localStorage.setItem("products",JSON.stringify(this.data));
        }
      })
      if(!isFound){
        product.qty=1;
        this.data.push(product);
        localStorage.setItem("products",JSON.stringify(this.data));
      }
    }else{
      product.qty=1;
      this.data.push(product);
      localStorage.setItem("products",JSON.stringify(this.data));
    }

  }


}
