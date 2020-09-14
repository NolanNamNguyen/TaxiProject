import { InformDialogComponent } from 'src/app/shared/components/inform-dialog/inform-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { Schedule } from './../../../../shared/models/schedule';
import { CustomerService } from './../../services/customer.service';
import { Province } from './../../../../shared/models/provinceList';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ComboBoxComponent } from '@syncfusion/ej2-angular-dropdowns';
import { DomSanitizer } from '@angular/platform-browser';
import { scheduled } from 'rxjs';

@Component({
  selector: 'app-search-booking',
  templateUrl: './search-booking.component.html',
  styleUrls: ['./search-booking.component.scss'],
})
export class SearchBookingComponent implements OnInit {
  submited = false;
  searchForm: FormGroup;
  driverImg: string;
  scheduleList: Schedule[];
  @ViewChild('startProvince')
  public startProvinceBox: ComboBoxComponent;
  @ViewChild('endProvince')
  public endProvinceBox: ComboBoxComponent;

  //Property prepare for combobox
  searchProvinceList = this.province.getProvinceList();
  // maps the appropriate column to fields property
  public fields: Object = { text: 'provinceName', value: 'provinceKey' };
  public height: string = '250px';
  public value: string = '';
  public waterMark1: string = 'Start Province';
  public waterMark2: string = 'Destination Province';
  public fields2: Object = { text: 'provinceName', value: 'provinceKey' };
  public value2: string = '';
  //Property prepare for combobox

  constructor(
    private province: Province,
    private formBuilder: FormBuilder,
    private cusService: CustomerService,
    private sanitizer: DomSanitizer,
    public dialog: MatDialog,

  ) {}

  ngOnInit(): void {
    this.searchForm = this.formBuilder.group({
      reservation: ['', Validators.required],
    });
  }

  onComboboxChange(args: any): void {
    if (this.startProvinceBox.text) {
      let myString = this.startProvinceBox.text.toString();
      let myvalue = this.startProvinceBox.value.toString();
      console.log(myString);
    }
    if (this.endProvinceBox.text) {
      let myString = this.endProvinceBox.text.toString();
      let myvalue = this.endProvinceBox.value.toString();
      console.log(myString);
    }
  }

  orderSchedule(selectedScheduleId: number){
    let orderedSchedule = this.scheduleList.filter(s => s.scheduleId == selectedScheduleId)[0];
    let body = {
      pickupLocation: orderedSchedule.startProvince,
      returnLocation: orderedSchedule.destinationProvince,
      reservations: this.searchForm.controls.reservation.value,
      driverId: orderedSchedule.driverId.toString()
    }
    this.cusService.bookDriver(body).subscribe((respone)=>{
      console.log(respone);
    })
    this.cusService.scheduleOrder.next(orderedSchedule);
  }


  onSearch() {
    localStorage.getItem('currentUser')
    this.submited = true;
    if (this.searchForm.invalid || !this.startProvinceBox.value || !this.endProvinceBox.value) {
      const errorDialog = this.dialog.open(InformDialogComponent, {
        width: '380px',
        height: '180px',
        data: {
          isFailed: true,
          failedMessage: 'Please input all required information',
        },
      });
      return ;
    }
    const loadingDialog = this.dialog.open(InformDialogComponent, {
      width: '380px',
      height: '250px',
      data: {
        isLoading: true,
        loadingMessage: 'Loding Data...',
      },
    });
    let seachFormbody = {
      pickupLocation : this.startProvinceBox.text.toString(),
      returnLocation : this.endProvinceBox.text.toString(),
      reservations : this.searchForm.controls.reservation.value,
    }

    this.cusService.searchSchedule(seachFormbody).subscribe(
      (dataResponse)=>{
        console.log(dataResponse);
        loadingDialog.close();
        this.scheduleList = dataResponse;
        console.log(this.scheduleList);
      },
      (error)=>{
        const errorDialog = this.dialog.open(InformDialogComponent, {
          width: '380px',
          height: '180px',
          data: {
            isFailed: true,
            failedMessage: "Sorry something happend!"
          },
        });
        console.log(error);
        window.alert(error);
      }
    )
  }
}
