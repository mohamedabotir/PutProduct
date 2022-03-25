import { ToastrService } from 'ngx-toastr';

import { CartService } from './../Services/cart-service.service';
import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Product } from 'src/Shared/Products';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit,OnChanges {
  @Input() qty!:Number;
  constructor(private cartService:CartService,private toast:ToastrService) { }
  ngOnChanges(changes: SimpleChanges): void {
    console.log(this.products,changes+"saddsadsadsadasd");
  }
  products!:Product[];

  ngOnInit(): void {
   this.products = this.cartService.returnProducts();
     console.log(this.cartService.data);
  }
  deleteItem(item:Product){
    this.cartService.Delete(item);
    console.log(item);
    for (let index = 0; index < this.products.length; index++) {
      if(this.products[index].id==item.id){
        this.products.splice(index,1);
        return;
      }
    }

  }
  pay(){
  this.cartService.Pay(this.products,"fdsfsdfsd");
}
increment(index:number){
  if(this.products[index].qty +1 >this.products[index].quantity)
  {this.toast.info(`maximum quantity from ${this.products[index].name.substring(0,28)} is ${this.products[index].quantity}`)}
  else
  this.products[index].qty += 1;
}
decrement(index:number){
  if(this.products[index].qty -1 ==0){
    return;
  }
  else
  this.products[index].qty -= 1;
}

}
