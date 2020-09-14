import { SharedModule } from 'src/app/shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DriverListRoutingModule } from './driver-list-routing.module';
import { DriverListComponent } from './driver-list.component';


@NgModule({
  declarations: [DriverListComponent],
  imports: [
    CommonModule,
    DriverListRoutingModule,
    SharedModule
  ]
})
export class DriverListModule { }
