import { CustomerService } from './services/customer.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './../../shared/shared.module';
import { AppLogoComponent } from 'src/app/shared/components/app-logo/app-logo.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ComboBoxModule } from '@syncfusion/ej2-angular-dropdowns';
import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerComponent } from './customer.component';
import { CustomerSidebarComponent } from './components/customer-sidebar/customer-sidebar.component';
import { CustomerProfileComponent } from './components/customer-profile/customer-profile.component';
import { SearchBookingComponent } from './components/search-booking/search-booking.component';
import { MyorderComponent } from './components/myorder/myorder.component';


@NgModule({
  declarations: [
    CustomerComponent,
    CustomerSidebarComponent,
    CustomerProfileComponent,
    SearchBookingComponent,
    MyorderComponent,
  ],
  imports: [
    CommonModule,
    CustomerRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    ComboBoxModule
  ],
  providers: [CustomerService]
})
export class CustomerModule {}
