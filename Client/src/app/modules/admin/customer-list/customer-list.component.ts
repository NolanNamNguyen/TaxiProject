import { DialogComponent } from './../../../shared/components/dialog/dialog.component';
import { AdminServiceService } from './../services/admin.service';
import { Customer } from './../../../shared/models/customer';
import { User } from './../../../shared/models/user';
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { SelectionModel } from '@angular/cdk/collections';
import { from } from 'rxjs';
import * as $ from 'jquery';
import { even } from '@rxweb/reactive-form-validators';
import { ClassGetter } from '@angular/compiler/src/output/output_ast';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { error } from 'console';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.scss'],
})
export class CustomerListComponent implements OnInit {
  customerList: Customer[];
  selectedCusId: string;
  selectedCustomer: Customer;

  displayedColumns: string[] = [
    'customerId',
    'address',
    'email',
    'name',
    'phone',
    'userName',
  ];
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  dataSource: any;

  constructor(private adService: AdminServiceService,public dialog: MatDialog) {}

  ngOnInit(): void {
    this.getDataforTable();
  }

  getDataforTable(){
    this.adService.getallCustomer().subscribe((cuslistResponse) => {
      this.customerList = cuslistResponse;
      this.dataSource = new MatTableDataSource(this.customerList);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
      setTimeout(() => {
        $(function(){
          $(".mat-paginator-page-size-label").text("Customers per page")
        })
      }, 250);
    });
  }

  rowSelected(event: Event){
    const parentNode = $(event.target).closest('.mat-row');
    this.selectedCusId = $(parentNode).find('.mat-column-customerId').text();
    this.adService.getCustomerById(this.selectedCusId).subscribe(customer => this.selectedCustomer = customer);
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      width: '350px',
      height:'200px',
      data: {isLoading: false, isDeleting: true}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      if(result){
        this.adService.deleteCustomerbyId(this.selectedCusId).subscribe(
          (response) =>{
            this.selectedCustomer = null;
            this.getDataforTable();
            console.log(response);
          },
          (error)=>{
            console.log(error);
          }
        )
      }
    });
  }
}
