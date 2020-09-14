import { Driver } from './../../../shared/models/driver';
import { Component, OnInit, ViewChild } from '@angular/core';
import { DialogComponent } from "./../../../shared/components/dialog/dialog.component";
import { AdminServiceService } from "./../services/admin.service";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { MatPaginator } from "@angular/material/paginator";
import { SelectionModel } from "@angular/cdk/collections";
import * as $ from "jquery";
import { MatDialog } from "@angular/material/dialog";

@Component({
  selector: 'app-driver-list',
  templateUrl: './driver-list.component.html',
  styleUrls: ['./driver-list.component.scss']
})
export class DriverListComponent implements OnInit {
  driverList: Driver[];
  selectedDriverId: string;
  selection: SelectionModel<Driver>;
  selectedDriver: Driver;

  displayedColumns: string[] = [
    'driverId',
    'address',
    'email',
    'name',
    'phone',
    'userName'
  ];
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  dataSource: any;

  constructor(private adService: AdminServiceService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getDataforTable();
  }

  getDataforTable(){
    this.adService.getallDriver().subscribe((driverListResponse) => {
      this.driverList = driverListResponse;
      this.dataSource = new MatTableDataSource(this.driverList);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
      const initialSelection = [];
      const allowMultiSelect = true;
      this.selection = new SelectionModel<Driver>(
        allowMultiSelect,
        initialSelection
      );
      setTimeout(() => {
        $(function(){
          $(".mat-paginator-page-size-label").text("Drivers per page")
        })
      }, 250);
    });
  }

  rowSelected(event: Event){
    const parentNode = $(event.target).closest('.mat-row');
    this.selectedDriverId = $(parentNode).find('.mat-column-driverId').text();
    this.adService.getDriverById(this.selectedDriverId).subscribe(driver => this.selectedDriver = driver);
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      width: '350px',
      height:'200px',
      data: {isLoading: false, isDeleting: true}
    });
    
    dialogRef.afterClosed().subscribe(result => {
      if(result){
        this.adService.deleteDriverbyId(this.selectedDriverId).subscribe(
          (response) => {
            this.selectedDriver = null;
            this.getDataforTable();
          },
          (error) => {
            console.log(error);
          }
        )
      }
    });
  }
}
