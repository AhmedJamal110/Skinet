import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';


@Injectable({
  providedIn: 'root'
})
export class BusyService {

  busyCount = 0;
  constructor( private _NgxSpinnerService:NgxSpinnerService ) { }


  busy(){
    this.busyCount ++;
    this._NgxSpinnerService.show(undefined , {
      type : 'timer',
      bdColor : 'rbga(255,255,255,0.7)',
      color : '#333333'
    })
  }

  idle(){
    this.busyCount --;
    if(this.busyCount <= 0){
      this._NgxSpinnerService.hide();
    }
  }



}
