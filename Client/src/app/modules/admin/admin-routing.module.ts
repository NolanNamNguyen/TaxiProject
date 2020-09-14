import { CustomerListComponent } from './customer-list/customer-list.component';
import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AdminComponent } from './admin.component';
import { SideBarComponent } from './sidbar/side-bar.component';
import { DashboardComponent } from './dashboard/dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: AdminComponent,
    children: [
      // {path:'user-list', component: NavbarComponent}
      { path: 'dashboard', loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule) },
      // { path: 'user_list', loadChildren: () => import('./customer-list/customer-list.module').then(m => m.CustomerListModule) },
      { path: 'user_list', component: CustomerListComponent },
      { path: 'driver_list', loadChildren: () => import('./driver-list/driver-list.module').then(m => m.DriverListModule) },
      { path: 'order_list', loadChildren: () => import('./order-list/order-list.module').then(m => m.OrderListModule) },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
