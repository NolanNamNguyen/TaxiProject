import { CustomerListModule } from './customer-list/customer-list.module';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { AdminRoutingModule } from './admin-routing.module';
import { SideBarComponent } from './sidbar/side-bar.component';
import { AppLogoComponent } from 'src/app/shared/components/app-logo/app-logo.component';
import { RouterModule } from '@angular/router';
import { DashboardModule } from './dashboard/dashboard.module';

@NgModule({
  declarations: [
    AdminComponent,
    SideBarComponent,
  ],
  imports: [
    CommonModule, 
    AdminRoutingModule,
    SharedModule,
    RouterModule,
    DashboardModule,
    SharedModule,
    CustomerListModule
  ],
})
export class AdminModule {}
