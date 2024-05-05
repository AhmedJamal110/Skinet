import { Component } from '@angular/core';
import { BreadcrumbService } from 'xng-breadcrumb';



@Component({
  selector: 'app-header-section',
  templateUrl: './header-section.component.html',
  styleUrls: ['./header-section.component.css']
})
export class HeaderSectionComponent {

  constructor(public _BreadcrumbService: BreadcrumbService){}

}
