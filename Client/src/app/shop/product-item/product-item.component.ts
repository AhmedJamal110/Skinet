import { BasketService } from 'src/app/baket/basket.service';
import { Product } from './../../shared/models/Products';
import { Component, Input } from '@angular/core';


@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.css']
})
export class ProductItemComponent {

  @Input() Products? : Product;

constructor( private _BasketService : BasketService){}


  addItemsToBasket(){
    this.Products && this._BasketService.addItemToBasket(this.Products);
  }

}
