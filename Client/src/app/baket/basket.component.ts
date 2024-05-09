import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { BasketItem } from '../shared/models/Basket';


@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent {

constructor( public _BasketService : BasketService ){}


  incementQuntity( item : BasketItem){
    this._BasketService.addItemToBasket(item)
  }


removeQuntity( id : number , quntity: number){

  this._BasketService.removeItemFrombasket(id , quntity)
}

}


