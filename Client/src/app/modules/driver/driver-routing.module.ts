import { DriverOrderlistComponent } from './components/driver-orderlist/driver-orderlist.component';
import { ProfilePageComponent } from './components/profile-page/profile-page.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DriverComponent } from './driver.component';
import { VehicleComponent } from './components/vehicle/vehicle.component';
import { ScheduleComponent } from './components/schedule/schedule.component';

const routes: Routes = [
  {
    path: '',
    component: DriverComponent,
    children: [
      { path: 'profile_page', component: ProfilePageComponent },
      { path: 'vehicle', component: VehicleComponent },
      { path: 'schedule', component: ScheduleComponent },
      { path: 'order_list', component: DriverOrderlistComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DriverRoutingModule {}
