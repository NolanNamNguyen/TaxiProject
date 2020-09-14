import { DriverOrderlistComponent } from './components/driver-orderlist/driver-orderlist.component';
import { TaxiDatePickerModule } from './taxiDatapicker.module';
import { DriverService } from './services/driver.service';
import { ProfilePageComponent } from './components/profile-page/profile-page.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DriverRoutingModule } from './driver-routing.module';
import { DriverComponent } from './driver.component';
import { ScheduleComponent } from './components/schedule/schedule.component';
import { VehicleComponent } from './components/vehicle/vehicle.component';
import { FormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { DriverSidebarComponent } from './components/driver-sidebar/driver-sidebar.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ComboBoxModule } from '@syncfusion/ej2-angular-dropdowns';

@NgModule({
  declarations: [
    DriverComponent,
    ScheduleComponent,
    VehicleComponent,
    DriverSidebarComponent,
    ProfilePageComponent,
    DriverOrderlistComponent
  ],
  imports: [
    CommonModule,
    DriverRoutingModule,
    FormsModule,
    SharedModule,
    ReactiveFormsModule,
    ComboBoxModule,
    TaxiDatePickerModule,
    SharedModule,
  ],
  providers: [DriverService],
})
export class DriverModule {}
