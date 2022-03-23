import { CartService } from './../Services/cart-service.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  constructor(private cartService:CartService) { }
  products:any;

  ngOnInit(): void {
     console.log(this.cartService.data);


  }

}
