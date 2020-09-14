import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DriverListComponent } from './driver-list.component';

const routes: Routes = [{ path: '', component: DriverListComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DriverListRoutingModule { }
