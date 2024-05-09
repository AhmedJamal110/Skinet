import { Component } from '@angular/core';
import { BasketService } from 'src/app/baket/basket.service';


@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.css']
})
export class OrderTotalsComponent {

  constructor( public _BasketService: BasketService ){}

}
