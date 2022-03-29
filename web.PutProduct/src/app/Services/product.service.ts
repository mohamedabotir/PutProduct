import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Product } from 'src/Shared/Products';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
apiPath = environment.url;
  constructor(private push:HttpClient,private Auth:AuthService) {


  }
pushProduct(data:Product):Observable<Product>{
return this.push.post<Product>(this.apiPath+"product/create",data);
}
getProducts():Observable<Product[]>{
  return this.push.get<Product[]>(this.apiPath+"product/products");
}
getProduct(id:any):Observable<Product>{
return this.push.get<Product>(this.apiPath+"product/"+id);
}
deleteProduct(id:Number):Observable<Product>{
  return this.push.delete<Product>(this.apiPath+"product/RemoveProduct/"+id);
}
updateProduct(product:Product):Observable<Product>{
  return this.push.put<Product>(this.apiPath+"product/Update",product);
}
}
