import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/Pagination';
import { Product } from '../shared/models/Products';
import { Brands } from '../shared/models/Brands';
import { Types } from '../shared/models/Types';


@Injectable({
  providedIn: 'root'
})
export class ShopService {

BaseUrl : string = `https://localhost:7277/api/`;



constructor(private _HttpClient:HttpClient) { }

  getProducts(brandsId? : number , typesId? : number , sort? :string)
  {
    let params = new HttpParams();
    if(brandsId) params = params.append("brandId" , brandsId)
    if(typesId) params = params.append("typesId" , typesId)
    if(sort) params = params.append("sort" , sort)

    return this._HttpClient.get<Pagination<Product[]>>(this.BaseUrl + `products`, {params})
  }

  getBrands()
  {
      return this._HttpClient.get<Brands[]>(this.BaseUrl +`Products/brands`);
  }
  getTypes()
  {
    return this._HttpClient.get<Types[]>(this.BaseUrl +`Products/types`);
 }

}
