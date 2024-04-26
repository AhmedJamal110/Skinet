import { ShopParams } from './../shared/models/shopParams';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
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
  @ViewChild("search") searchTarem? : ElementRef;


  ProductsData: Product[] = [];
  brandsDat : Brands[] = [];
  typesData : Types[] =[];
 ShopParams = new ShopParams();

totalCount = 0;
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
      this._shopService.getProducts(this.ShopParams).subscribe({
        next:(response) =>{
         this.ProductsData = response.data,
         this.ShopParams.pageNumber = response.pageIndex;
         this.ShopParams.pageSize = response.pageSize;
         this.totalCount = response.count;


        },

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
    this.ShopParams.brandId = brandId;
    this.GetAllProducts();

  }
  onTypesIdSelected(typeId : number){
    this.ShopParams.typeId = typeId;
    this.GetAllProducts();

  }

    onSortedSelected(event : any ){
      this.ShopParams.sort = event.target.value;
      this.GetAllProducts();
    }

    onPageChanged(event : any){
      if(this.ShopParams.pageNumber !== event){

        this.ShopParams.pageNumber = event;

        this.GetAllProducts();

      }



    }

     onSearchButton(){

      this.ShopParams.search = this.searchTarem?.nativeElement.value;

      this.GetAllProducts();
     }

     onResetButton(){
        if(this.searchTarem)
            this.searchTarem.nativeElement.value = '';

        this.ShopParams = new ShopParams();
        this.GetAllProducts();

     }

}





