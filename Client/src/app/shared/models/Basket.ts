import * as Cuid from 'cuid'
export interface Basket {
  id: string
  items: BasketItem[]
}

export interface BasketItem {
  id: number
  name: string
  price: number
  quntity: number
  pictureUrl: string
  brand: string
  type: string
}

export class Basket implements Basket{
  id = Cuid() ;
  items: BasketItem[] = []
}

  export interface BasketTotal
  {
    shipping : number;
    subtotal : number,
    total : number ;


  }
