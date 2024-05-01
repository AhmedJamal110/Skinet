import { HttpClient } from '@angular/common/http';
import { enviroment } from './../../../enviroment/enviroment';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {

  baseUrl = enviroment.apiUrl;
  validationError : string[] = [];

constructor(private _http:HttpClient){}

  ngOnInit(): void {
  //  this.get404Error()
  //  this.get500Error()
  //  this.get400Error()
  //  this.get400ValidationError()


  }

  get404Error(){
    this._http.get(this.baseUrl +'products/100').subscribe({
      next:(response) => console.log(response),
      error:(error) => console.log(error)
    })
  }

  get500Error(){
    this._http.get(this.baseUrl +`Buggy/serverError`).subscribe({
      next:(response) => console.log(response),
      error: (err) => console.log(err)
    })
  }
  get400Error(){
    this._http.get(this.baseUrl +`Buggy/badrequest`).subscribe({
      next:(response) => console.log(response),
      error: (err) => console.log(err)
    })
  }

  get400ValidationError(){
    this._http.get(this.baseUrl +`products/hundered`).subscribe({
      next:(response) => console.log(response),
      error: (err) => {
        console.log(err);
        this.validationError = err.error


      }
    })
  }


}
