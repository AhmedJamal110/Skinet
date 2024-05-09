import { BasketItem, BasketTotal } from './../shared/models/Basket';
import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { enviroment } from 'src/enviroment/enviroment';
import { Basket } from '../shared/models/Basket';
import { BehaviorSubject } from 'rxjs';
import { Product } from '../shared/models/Products';


@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = enviroment.apiUrl;
  private basketSource = new BehaviorSubject<Basket | null>(null)
  basketSource$ = this.basketSource.asObservable();

  private basketTotal = new BehaviorSubject<BasketTotal|null>(null);
  basketTotalSource$ = this.basketTotal.asObservable();

  constructor(private _HttpClient:HttpClient ) { }


  getBasketByid( id : string )
  {
   return this._HttpClient.get<Basket>(this.baseUrl +'basket?basketId='+id).subscribe({
      next:(response) =>{
      this.basketSource.next(response),
      this.CalculateTotal();

      } ,
    })
  }

  setBasket( basket : Basket){
    console.log(basket);

     return this._HttpClient.post<Basket>(this.baseUrl+'basket', basket).subscribe({
      next:(response) => {

        this.basketSource.next(response)

        this.CalculateTotal()

      },

    })
  }


  getCurrentBasket(){

   return this.basketSource.value
  }

  addItemToBasket(item : Product | BasketItem, quntity = 1){
    if(this.IsProducrt(item))
       item = this.mappedProductItemToBasketItem(item)
    const basket = this.getCurrentBasket() ?? this.createBasket()
    basket.items = this.addOrUpadteBasket(basket.items , item , quntity)
    this.setBasket(basket);
}

removeItemFrombasket( id : number , quntity = 1){
 var basket = this.getCurrentBasket();
 if(!basket)
    return;
  var item = basket.items.find(I=> I.id === id)
  if(item){
    item.quntity -= quntity;
    if(item.quntity === 0){
      basket.items = basket.items.filter( I => I.id !== id)
    }
    if(basket.items.length > 0){
      this.setBasket(basket);
    }else{
          this.deleteBasket(basket)
    }
  }

}

  deleteBasket(basket : Basket){
    this._HttpClient.delete(this.baseUrl+'basket?basketId='+basket.id).subscribe({
      next:(response) =>{

        this.basketSource.next(null);
        this.basketTotal.next(null);
        localStorage.removeItem('basket_id')


      }



    })
  }

  private addOrUpadteBasket(items: BasketItem[] , itemToAdd: BasketItem , quntity: number):BasketItem[]{
    const item =  items.find(x => x.id == itemToAdd.id)
    if(item){
      item.quntity += quntity
    }
    else{
      itemToAdd.quntity = quntity;
      items.push(itemToAdd)
    }
    return items

  }


  private createBasket() : Basket{
    const basket = new Basket()
    localStorage.setItem('basket_id' , basket.id)
    return basket
  }

  private mappedProductItemToBasketItem(item: Product):BasketItem{
    return {
      id : item.id,
      name : item.name,
      price : item.price,
      quntity : 0,
      pictureUrl : item.pictureUrl,
      brand : item.productBrand,
      type : item.productType,

    }

  }


private CalculateTotal(){
     var basket = this.getCurrentBasket();
  if(!basket)
      return;
    const shipping =0;
    const subtotal = basket.items.reduce((a,P) => (P.price * P.quntity) + a , 0)
    const total = subtotal + shipping;
    this.basketTotal.next({ shipping, subtotal , total })


  }

private IsProducrt(item : Product | BasketItem) :item is Product{
  return ( item as Product).productBrand !== undefined;
}

}
