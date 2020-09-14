import { CustomerAdapter } from './../../../shared/models/customer';
import { Order, OrderAdapter } from './../../../shared/models/order';
import { Schedule, ScheduleAdapter } from './../../../shared/models/schedule';
import { CustomerModule } from './../customer.module';
import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError, of } from 'rxjs';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { catchError, retry } from 'rxjs/operators';
import { map } from 'rxjs/operators';
import { Customer } from 'src/app/shared/models/customer';

@Injectable()
export class CustomerService {
  scheduleList: Schedule[] = new Array();
  scheduleOrder: BehaviorSubject<Schedule> = new BehaviorSubject(null);
  orderList: Order[];
  currentOrder: Order;
  orderById: Order;
  myProfile: Customer;
  constructor(
    private http: HttpClient, 
    private scheduleAdapter: ScheduleAdapter,
    private orderAdapter: OrderAdapter,
    private cusAdapter: CustomerAdapter,
    ) {}

  searchSchedule(scheduleForm: any): Observable<any> {
    this.scheduleList = [];
    return this.http.post(
      `${environment.apiUrl}/api/customers/schedules/search`,
      scheduleForm
    ).pipe(map((data: any[])=>{
      console.log(data);
      data.map((item) =>{
        this.scheduleList.push(this.scheduleAdapter.adapt(item));
      })
      return this.scheduleList;
    }));
  }

  bookDriver(bookForm: any): Observable<any>{
    return this.http.post(
      `${environment.apiUrl}/api/customers/booking`,
      bookForm
    );
  }

  getAllOrders(){
    this.orderList = [];
    return this.http.get(
      `${environment.apiUrl}/api/customers/orders`
    ).pipe(map((data: any[])=>{
      data.map((item)=>{
        // console.log(item);
        this.orderList.push(this.orderAdapter.adapt(item));
        this.currentOrder = this.orderList.filter(order => order.status == false && order.isCancel == false)[0];
      })
      console.log(this.orderList);
      return this.orderList;
    }))
  }

  getCurrentOrder(customerId: string){
    return this.http.get(
      `${environment.apiUrl}/api/customers/currentorder/${customerId}`
    ).pipe(map((resData) => {
      return this.currentOrder = this.orderAdapter.adapt(resData);
    }))
  }

  getProfile(){
    return this.http.get(`${environment.apiUrl}/api/customers/profile`).pipe(map(customer =>{
      this.myProfile = this.cusAdapter.adapt(customer);
      return this.myProfile;
    }));
  }

  removeOrder(orderId: number){
    let body = {};
    return this.http.put(`${environment.apiUrl}/api/customers/orders/cancel/${orderId}`, body);
  }
  
  getOrderbyOrderId(orderId: string){
    return this.http.get(
      `${environment.apiUrl}/api/customers/orders/${orderId}`
    ).pipe(map((resData) => {
      this.orderById = this.orderAdapter.adapt(resData);
      return this.orderById;
    }))
  }

  ratingOrder(orderId: string, body: any){
    return this.http.put(
      `${environment.apiUrl}/api/customers/orders/rating/${orderId}`,
      body
    )
  }
}
