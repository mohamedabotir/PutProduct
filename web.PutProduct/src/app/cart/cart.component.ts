
import { CartService } from './../Services/cart-service.service';
import { Component, OnInit } from '@angular/core';
import { Product } from 'src/Shared/Products';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  constructor(private cartService:CartService) { }
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

}
