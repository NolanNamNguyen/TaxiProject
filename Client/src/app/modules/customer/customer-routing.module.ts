import { MyorderComponent } from './components/myorder/myorder.component';
import { SearchBookingComponent } from './components/search-booking/search-booking.component';
import { CustomerProfileComponent } from './components/customer-profile/customer-profile.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CustomerComponent } from './customer.component';

const routes: Routes = [
  {
    path: '',
    component: CustomerComponent,
    children: [
      { path: 'profile_page',component: CustomerProfileComponent},
      { path: 'seach_booking',component: SearchBookingComponent},
      { path: 'my_order',component: MyorderComponent},
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CustomerRoutingModule {}
