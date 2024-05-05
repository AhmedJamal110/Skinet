import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { TestErrorsComponent } from './test-errors/test-errors.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { ToastrModule } from 'ngx-toastr';
import { HeaderSectionComponent } from './header-section/header-section.component';
import { BreadcrumbModule } from 'xng-breadcrumb';
import { NgxSpinnerModule } from 'ngx-spinner';



@NgModule({
  declarations: [
    NavbarComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    HeaderSectionComponent

  ],
  imports: [
    CommonModule,
    RouterModule,
    ToastrModule.forRoot({
      positionClass : "toast-bottom-right",
      preventDuplicates : true
    }),

    BreadcrumbModule,
    NgxSpinnerModule

  ],
  exports : [
    NavbarComponent,
    HeaderSectionComponent,
    NgxSpinnerModule

  ]
})
export class CoreModule { }
