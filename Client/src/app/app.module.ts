import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CoreModule } from './core/core.module';
import { ShopModule } from './shop/shop.module';
import { HomeModule } from './home/home.module';
import { InterceptorsErrorsInterceptor } from './core/interceptors-errors.interceptor';
import { LoadingInterceptor } from './core/loading.interceptor';
import { BasketComponent } from './baket/basket.component';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    BasketComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    ShopModule,
    HomeModule,
    SharedModule
  ],
  providers: [
  {provide : HTTP_INTERCEPTORS ,
  useClass : InterceptorsErrorsInterceptor,
  multi : true
  },
  {provide : HTTP_INTERCEPTORS ,
    useClass: LoadingInterceptor,
    multi: true
  }

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
