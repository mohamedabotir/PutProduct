import { CartService } from './../Services/cart-service.service';
import { Product } from 'src/Shared/Products';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
 products!:Product[];
  constructor(private cart:CartService) { }

  ngOnInit(): void {
    this.products = this.cart.returnProducts();
    console.log(this.products);
  }

}
