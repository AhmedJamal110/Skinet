import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ShopComponent } from './shop/shop.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { TestErrorsComponent } from './core/test-errors/test-errors.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';

const routes: Routes = [
  {path:'' ,  component : HomeComponent , data: {breadcrumb : 'Home'}  },
  {path:'home' ,  component : HomeComponent},
  {path:'not-found' ,  component : NotFoundComponent},
  {path:'server-error' ,  component : ServerErrorComponent},
  {path:'test-errors' ,  component : TestErrorsComponent},
  {path:'shop' , loadChildren:() => import('./shop/shop.module').then(m => m.ShopModule)},
  {path:'**' , redirectTo : ''  , pathMatch:'full'},


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
