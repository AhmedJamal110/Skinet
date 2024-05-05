import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PaginationModule} from 'ngx-bootstrap/pagination';
import { PaginationHeaderComponent } from './pagination-header/pagination-header.component';
import { PaggerComponent } from './pagger/pagger.component';
import { CarouselModule } from "ngx-bootstrap/carousel";



@NgModule({
  declarations: [
    PaginationHeaderComponent,
    PaggerComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot()
  ],
  exports: [
    PaginationModule,
    PaginationHeaderComponent,
    PaggerComponent,
    CarouselModule
  ]
})
export class SharedModule { }
