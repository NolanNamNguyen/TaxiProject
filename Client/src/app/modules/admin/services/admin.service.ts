import { DriverAdapter } from './../../../shared/models/driver';
import { Customer } from 'src/app/shared/models/customer';
import { CustomerAdapter } from './../../../shared/models/customer';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Driver } from 'src/app/shared/models/driver';


@Injectable({
  providedIn: 'root'
})
export class AdminServiceService {
  driverList: Driver[];
  customerList: Customer[];

  constructor(private http: HttpClient,private cusadapter: CustomerAdapter, private driveradapter: DriverAdapter) { }

  getallCustomer(){
    this.customerList = [];
    // using string interpolation
    return this.http.get(
      `${environment.apiUrl}/api/admins/customers`
    ).pipe(map((data: any[])=>{
      data.map((item)=>{
        // console.log(item);
        this.customerList.push(this.cusadapter.adapt(item));
      })
      return this.customerList;
    }))
  }

  getCustomerById(cusId: string){
    // using string interpolation
    return this.http.get(
      `${environment.apiUrl}/api/admins/customers/${cusId}`
    ).pipe(map((data: any)=>{
      return this.cusadapter.adapt(data);
    }))
  }

  deleteCustomerbyId(cusId: string){
    return this.http.delete(
      `${environment.apiUrl}/api/admins/customers/${cusId}`
    );
  }

  getallDriver(){
    this.driverList = [];
    // using string interpolation
    return this.http.get(
      `${environment.apiUrl}/api/admins/drivers`
    ).pipe(map((data: any[])=>{
      data.map((item)=>{
        // console.log(item);
        this.driverList.push(this.driveradapter.adapt(item));
      })
      return this.driverList;
    }))
  }

  getDriverById(driverId: string){
    // using string interpolation
    return this.http.get(
      `${environment.apiUrl}/api/admins/drivers/${driverId}`
    ).pipe(map((data: any)=>{
      return this.driveradapter.adapt(data);
    }))
  }

  deleteDriverbyId(driverId: string){
    return this.http.delete(
      `${environment.apiUrl}/api/admins/drivers/${driverId}`
    );
  }

}
