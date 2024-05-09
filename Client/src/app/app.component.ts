import { Component, OnInit } from '@angular/core';
import { BasketService } from './baket/basket.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  implements OnInit {
  title = 'Client';

constructor(private _BasketService: BasketService ){}
  ngOnInit(): void {

    const basketId = localStorage.getItem('basket_id');
    if(basketId){
      this._BasketService.getBasketByid(basketId)
    }


  }



  }



