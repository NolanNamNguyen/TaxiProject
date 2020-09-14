import { Vehicle } from './../../../../shared/models/vehicle';
import { MatDialog } from '@angular/material/dialog';
import { tap } from 'rxjs/operators';
import { ScheduleAdapter } from './../../../../shared/models/schedule';
import { Customer } from './../../../../shared/models/customer';
import { Province } from './../../../../shared/models/provinceList';
import { DriverService } from './../../services/driver.service';
import { AuthService } from './../../../../core/auth/Auth.service';
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ComboBoxComponent } from '@syncfusion/ej2-angular-dropdowns';
import * as $ from 'jquery';
import { containsValidatorExtension } from '@rxweb/reactive-form-validators/validators-extension';
import { Schedule } from 'src/app/shared/models/schedule';
import { InformDialogComponent } from 'src/app/shared/components/inform-dialog/inform-dialog.component';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.scss'],
})
export class ScheduleComponent implements OnInit {
  mySchedule: Schedule;
  scheduleForm: FormGroup;
  submitted = false;
  myVehicle: Vehicle;
  @ViewChild('startProvince')
  public startProvinceBox: ComboBoxComponent;
  @ViewChild('endProvince')
  public endProvinceBox: ComboBoxComponent;

  //Property prepare for combobox
  scheduleProvinceList = this.province.getProvinceList();
  // maps the appropriate column to fields property
  public fields: Object = { text: 'provinceName', value: 'provinceKey' };
  public height: string = '250px';
  public value: number;
  public waterMark1: string = 'Start Province';
  public waterMark2: string = 'Destination Province';
  public fields2: Object = { text: 'provinceName', value: 'provinceKey' };
  public value2: number;
  //Property prepare for combobox
  minDate: Date;

  get submitScheduleForm() {
    return this.scheduleForm.controls;
  }

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private auth: AuthService,
    private driverSer: DriverService,
    private province: Province,
    public dialog: MatDialog
  ) {
    if(this.driverSer.currentVehicle){
      this.driverSer.currentVehicle.subscribe(vehicle => this.myVehicle = vehicle);
    }
  }

  ngOnInit(): void {
    const loadingDialog = this.dialog.open(InformDialogComponent, {
      width: '380px',
      height: '250px',
      disableClose: true,
      data: {
        isLoading: true,
        loadingMessage: 'Loding Data...',
      },
    });
    this.getCurrentSchedule().subscribe(
      (res) => {
        loadingDialog.close();
        this.buildForm();
      },
      (error) => {
        loadingDialog.close();
        this.buildForm();
      }
    );
  }

  buildForm() {
    this.scheduleForm = this.formBuilder.group({
      startAddress: [''],
      destinationAddress: [''],
      availableSeat: ['', Validators.required],
      startDate: ['', Validators.required],
    });
    this.minDate = new Date();
  }

  getCurrentSchedule() {
    return this.driverSer
      .getCurrentSchedule(this.auth.getDriverId().toString())
      .pipe(
        tap(
          (dataResponse) => {
            this.mySchedule = dataResponse;
          },
          (error) => {
            this.mySchedule = null;
            console.log(error);
          }
        )
      );
  }

  getDateValue(e: any) {
    let date = $('#startDate').val();
    console.log(e);
    console.log('abc');
    console.log(date);
  }

  submitForm(f: NgForm) {
    window.alert('test');
    console.log(f);
  }

  onSubmit() {
    console.log(this.startProvinceBox.value);
    this.submitted = true;

    if (this.scheduleForm.invalid) {
      console.log('fail');
      return;
    }

    if (!this.startProvinceBox.value || !this.endProvinceBox.value) {
      window.alert('please input start and destinatio nprovince');
      return;
    }
    if (Math.abs(this.startProvinceBox.value - this.endProvinceBox.value) > 2) {
      const errorDialog = this.dialog.open(InformDialogComponent, {
        width: '380px',
        height: '180px',
        data: {
          isFailed: true,
          failedMessage: 'Your input Province is too far apart',
        },
      });
      return;
    }
    try {
      if(this.myVehicle){
        let availableSeat: number = +(this.submitScheduleForm.availableSeat.value)
        if(availableSeat >= this.myVehicle.seater){
          const errorDialog = this.dialog.open(InformDialogComponent, {
            width: '380px',
            height: '180px',
            data: {
              isFailed: true,
              failedMessage: 'You have input the wrong available seat',
            },
          });
          return;
        }
      }
    } catch (error) {
      
    }
    let body = {
      startAddress: this.submitScheduleForm.startAddress.value,
      startProvince: this.startProvinceBox.text.toString(),
      destinationAddress: this.submitScheduleForm.destinationAddress.value,
      destinationProvince: this.endProvinceBox.text.toString(),
      availablableSlot: this.submitScheduleForm.availableSeat.value,
      startTime: this.submitScheduleForm.startDate.value,
      driverId: this.auth.getDriverId().toString(),
    };
    const loadingDialog = this.dialog.open(InformDialogComponent, {
      width: '380px',
      height: '200px',
      disableClose: true,
      data: {
        isLoading: true,
        loadingMessage: 'Registering the schedule...',
      },
    });
    this.driverSer.registerSchedule(body).subscribe(
      (dataResponse) => {
        loadingDialog.close();
        console.log(dataResponse);
        this.ngOnInit();
      },
      (error) => {
        loadingDialog.close();
        const errorDialog = this.dialog.open(InformDialogComponent, {
          width: '380px',
          height: '180px',
          data: {
            isFailed: true,
            failedMessage: 'Please register your vehicle',
          },
        });
      }
    );
  }

  goBack() {
    this.router.navigate(['/driver']);
  }

  deleteSchedule() {
    const loadingDialog = this.dialog.open(InformDialogComponent, {
      width: '380px',
      height: '200px',
      data: {
        isLoading: true,
        isSuccess: false,
        isFailed: false,
      },
    });
    this.driverSer
      .removeSchedule(this.auth.getDriverId().toString())
      .subscribe((resdata) => {
        console.log(resdata);
        loadingDialog.close();
        this.submitted = false;
        this.ngOnInit();
      });
  }
}
