import { Product } from 'src/Shared/Products';
export interface Order{
products?:Product[],
totalPrice?:Number,
discountCode?:string
}
