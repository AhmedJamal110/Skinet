import { ShopParams } from './../shared/models/shopParams';
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

  BaseUrl: string = `https://localhost:7277/api/`;



  constructor(private _HttpClient: HttpClient) { }

  getProducts(ShopParams: ShopParams) {
    let params = new HttpParams();
    if (ShopParams.brandId > 0) params = params.append("brandId", ShopParams.brandId)
    if (ShopParams.typeId > 0) params = params.append("typeId", ShopParams.typeId)
    params = params.append("sort", ShopParams.sort);
    params = params.append("pageIndex", ShopParams.pageNumber);
    if(ShopParams.search) params = params.append("Search", ShopParams.search);

  params = params.append("pageSize", ShopParams.pageSize);

    return this._HttpClient.get<Pagination<Product[]>>(this.BaseUrl + `products`, { params })
  }

  getBrands() {
    return this._HttpClient.get<Brands[]>(this.BaseUrl + `Products/brands`);
  }
  getTypes() {
    return this._HttpClient.get<Types[]>(this.BaseUrl + `Products/types`);
  }

}
