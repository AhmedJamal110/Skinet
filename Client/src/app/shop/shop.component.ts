import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { Product } from '../shared/models/Products';
import { Brands } from '../shared/models/Brands';
import { Types } from '../shared/models/Types';


@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {
  ProductsData: Product[] = [];
  brandsDat : Brands[] = [];
  typesData : Types[] =[];
  brandIdSelected  =0;
  typeIdSelected  =0;
  sortSelected = "name"
  sortOptions = [
    {name:"Alphabitc", value:"name"},
    {name:"price high to low" , value:"priceAsc"},
    {name:"price low to high" , value:"priceDesc"}

  ]


  constructor(private _shopService: ShopService) {}

  ngOnInit(): void {
    this.GetAllProducts();
    this.GetAllBrands();
    this.GetAllTypes();

  }


    GetAllProducts(){
      this._shopService.getProducts(this.brandIdSelected , this.typeIdSelected , this.sortSelected).subscribe({
        next:(response) => this.ProductsData = response.data,
        error:(err) => console.warn(err),
      })
    }

    GetAllBrands(){
      this._shopService.getBrands().subscribe({
        next:(response) => this.brandsDat = [{id:0 , name:"ALl Brands"}, ...response ],
        error:(err) => console.log(err)

      })
    }


GetAllTypes(){
  this._shopService.getTypes().subscribe({
    next:(response) => this.typesData =  [{id:0 , name:"All Types"} , ...response ],
    error:(err) => console.log(err)

  })
  }


  onBrandsIdSelected(brandId : number){
    this.brandIdSelected = brandId;
    this.GetAllProducts();

  }
  onTypesIdSelected(typeId : number){
    this.typeIdSelected = typeId;
    this.GetAllProducts();

  }

    onSortedSelected(event : any ){
      this.sortSelected = event.target.value;
      this.GetAllProducts();
    }

}





