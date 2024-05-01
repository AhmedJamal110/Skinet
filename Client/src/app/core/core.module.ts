import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { TestErrorsComponent } from './test-errors/test-errors.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { ToastrModule } from 'ngx-toastr';



@NgModule({
  declarations: [
    NavbarComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent

  ],
  imports: [
    CommonModule,
    RouterModule,
    ToastrModule.forRoot({
      positionClass : "toast-bottom-right",
      preventDuplicates : true
    })

  ],
  exports : [
    NavbarComponent
  ]
})
export class CoreModule { }
