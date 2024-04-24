import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Pagination } from './models/Pagination';
import { Product } from './models/Products';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Client';
  products: Product[] = [];
   constructor(private _HttpClient:HttpClient){}
  ngOnInit(): void {


    this._HttpClient.get<Pagination<Product[]>>(`https://localhost:7277/api/Products`).subscribe({
      next:(response ) => this.products = response.data,



      error:(error) => console.log(error),
      complete:() =>{
        console.log("try");
        console.log("OK");

      }


    })





  }

}
