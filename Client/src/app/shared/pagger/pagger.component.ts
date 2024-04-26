import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pagger',
  templateUrl: './pagger.component.html',
  styleUrls: ['./pagger.component.css']
})
export class PaggerComponent {

  @Input() totalCount? : number;
  @Input() pageSize? : number;

@Output() pageChanged = new EventEmitter<number>();


onPaggerChanged(event : any){

  this.pageChanged.emit(event.page);

}

}
