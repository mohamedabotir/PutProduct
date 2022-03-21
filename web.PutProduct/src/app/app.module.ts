import { ErrorInterceptorService } from './Services/error-interceptor.service';
import { ProductService } from './Services/product.service';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AuthService } from './Services/auth.service';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpInterceptor } from '@angular/common/http';
import { RegisterComponent } from './register/register.component';
import { ProductComponent } from './product/product.component';
import { InterceptorService } from './Services/interceptor.service';
import { ListProductsComponent } from './list-products/list-products.component';

import { ProductEditComponent } from './product-edit/product-edit.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { ProfileComponent } from './profile/profile.component';
import { CartService } from './Services/cart-service.service';
import { CartComponent } from './cart/cart.component';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    ProductComponent,
    ListProductsComponent,
    ProductEditComponent,
    ProfileComponent,
    CartComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [AuthService,ProductService,{
    provide:HTTP_INTERCEPTORS,
    useClass:InterceptorService,
    multi:true
  },{
    provide:HTTP_INTERCEPTORS,
    useClass:ErrorInterceptorService,
    multi:true
  },
  CartService


],
  bootstrap: [AppComponent]
})
export class AppModule { }
