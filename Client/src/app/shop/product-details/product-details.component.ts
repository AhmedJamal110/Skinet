import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { Product } from 'src/app/shared/models/Products';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';


@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {

  productDetail?: Product;


constructor(private _ShopService : ShopService, private _ActivatedRoute :ActivatedRoute , private _BreadcrumbService: BreadcrumbService)
{

  this._BreadcrumbService.set('@productDetails', ' ');
}

  ngOnInit(): void {

      this.getOneProduct();
  }

  getOneProduct(){
    const id = this._ActivatedRoute.snapshot.paramMap.get('id');
       if(id)
          this._ShopService.getSpecficationProduct(+id).subscribe({
     next: (response) =>
       {
        this.productDetail = response
        this._BreadcrumbService.set('@productDetails' , this.productDetail.name)
        },
     error : (err) => console.log(err),

    })

  }


}
