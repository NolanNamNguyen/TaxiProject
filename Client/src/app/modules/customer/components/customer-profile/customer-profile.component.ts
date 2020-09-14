import { DialogComponent } from './../../../../shared/components/dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { AdminServiceService } from './../../../admin/services/admin.service';
import { Order } from './../../../../shared/models/order';
import { Customer } from './../../../../shared/models/customer';
import { Component, OnInit, ViewChild, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, MatPaginatorIntl } from '@angular/material/paginator';
import { CustomerService } from '../../services/customer.service';
import { concatMap } from 'rxjs/operators';
import { of } from 'rxjs';
import * as $ from 'jquery';
import { InformDialogComponent } from 'src/app/shared/components/inform-dialog/inform-dialog.component';

@Component({
  selector: 'app-customer-profile',
  templateUrl: './customer-profile.component.html',
  styleUrls: ['./customer-profile.component.scss'],
})
export class CustomerProfileComponent implements OnInit, AfterViewInit {
  cusImage: any;
  selectedOrderId: string;
  selectedOrder: Order;
  customerInfo: Map<string, string> = new Map();
  myProfile: Customer;
  // order history table data
  historyOrderList: Order[];
  displayedColumns: string[] = [
    'orderId',
    'pickUpLocation',
    'returnLocation',
    'driverName',
    'rate',
    'created',
    'price',
  ];
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  orderDataSource: any;
  // order history table data

  constructor(private cusService: CustomerService, public dialog: MatDialog) {}

  ngOnInit(): void {
    const loadingDialog = this.dialog.open(InformDialogComponent, {
      width: '380px',
      height: '250px',
      data: {
        isLoading: true,
        loadingMessage: 'Loding Data...',
      },
    });
    this.cusService
      .getProfile()
      .subscribe((customer) => (this.myProfile = customer));
    this.cusService.getAllOrders().subscribe((orderlist) => {
      loadingDialog.close();
      this.historyOrderList = orderlist;
      this.orderDataSource = new MatTableDataSource(this.historyOrderList);
      this.orderDataSource.sort = this.sort;
      this.orderDataSource.paginator = this.paginator;
      setTimeout(() => {
        $(function(){
          $(".mat-paginator-page-size-label").text("Orders per page")
        })
      }, 250);
    });
  }
  ngAfterViewInit(){
    
  }
  rowSelected(event: Event) {
    const parentNode = $(event.target).closest('.mat-row');
    this.selectedOrderId = $(parentNode).find('.mat-column-orderId').text();
    this.cusService
      .getOrderbyOrderId(this.selectedOrderId)
      .subscribe((resData) => {
        this.selectedOrder = resData;
        if (this.selectedOrder.rate) {
          const alreadyRateDialog = this.dialog.open(InformDialogComponent, {
            width: '380px',
            height: '200px',
            data: {
              isLoading: false,
              isSuccess: false,
              isFailed: false,
              isAlreadyRate: true
            },
          });
          return;
        }
        this.openDialog();
      });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      width: '380px',
      height: '500px',
      data: {
        isLoading: false,
        isDeleting: false,
        isReviewing: true,
        driverName: this.selectedOrder.driverName,
        driverAvatar: this.selectedOrder.driverImg,
      },
    });

    dialogRef.afterClosed().subscribe((res) => {
      console.log('The dialog was closed');
      if (res) {
        console.log(res.rate);
        console.log(res.rateContent);
        let body = {
          rate: res.rate,
          rateContent: res.rateContent,
        };
        const loadingDialog = this.dialog.open(InformDialogComponent, {
          width: '380px',
          height: '200px',
          data: {
            isLoading: true,
            isSuccess: false,
            isFailed: false,
          },
        });
        this.cusService.ratingOrder(this.selectedOrderId, body).subscribe(
          (res) => {
            loadingDialog.close();
            const successDialog = this.dialog.open(InformDialogComponent, {
              width: '380px',
              height: '230px',
              data: {
                isLoading: false,
                isSuccess: true,
                isFailed: false,
                successMessage: 'successful rate driver',
              },
            });
            successDialog.afterClosed().subscribe((res) => {
              this.ngOnInit();
            });
          },
          (error) => {
            console.log(error);
          }
        );
      }
    });
  }
}
