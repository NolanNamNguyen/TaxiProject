import { ReviewAdapter, Review } from './../../../shared/models/review';
import { DriverAdapter, Driver } from './../../../shared/models/driver';
import { ScheduleAdapter, Schedule } from './../../../shared/models/schedule';
import { Vehicle, VehicleAdapter } from './../../../shared/models/vehicle';
import { Order, OrderAdapter } from './../../../shared/models/order';
import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { catchError, retry } from 'rxjs/operators';
import { map } from 'rxjs/operators';

@Injectable()
export class DriverService {
  orderList: Order[];
  vehicle: Vehicle;
  schedule: Schedule;
  driverProfile: Driver;
  reviewList: Review[];
  private currentVehicleSubject: BehaviorSubject<Vehicle>;
  public currentVehicle: Observable<Vehicle>;

  constructor(
    private http: HttpClient,
    private orderAdapter: OrderAdapter,
    private vehicleAdapter: VehicleAdapter,
    private scheduleAdapter: ScheduleAdapter,
    private driverAdapter: DriverAdapter,
    private reviewAdapter: ReviewAdapter
  ) {}

  registerVehicle(vehicleForm: any): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/api/drivers/vehicles/register`,
      vehicleForm
    );
  }

  registerSchedule(scheduleForm: any): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/api/drivers/schedules/register`,
      scheduleForm
    );
  }

  getAllOrders() {
    this.orderList = [];
    return this.http.get(`${environment.apiUrl}/api/drivers/orders`).pipe(
      map((data: any[]) => {
        console.log(data);
        data.map((item) => {
          this.orderList.push(this.orderAdapter.adapt(item));
        });
        return this.orderList;
      })
    );
  }

  completeOrder(orderId: string) {
    return this.http.put(
      `${environment.apiUrl}/api/drivers/orders/complete/${orderId}`,
      {}
    );
  }

  getVehicle(driverId: string) {
    return this.http
      .get(`${environment.apiUrl}/api/drivers/vehicles/${driverId}`)
      .pipe(
        map((data) => {
          this.vehicle = this.vehicleAdapter.adapt(data);
          this.currentVehicleSubject = new BehaviorSubject<Vehicle>(this.vehicle);
          this.currentVehicle = this.currentVehicleSubject.asObservable();
          this.currentVehicleSubject.next(this.vehicle);
          return this.vehicle;
        },
        (error) => {
          console.log(error);
        }
        )
      );
  }

  getCurrentSchedule(driverId: string) {
    return this.http
      .get(`${environment.apiUrl}/api/drivers/schedules/${driverId}`)
      .pipe(
        map((data) => {
          this.schedule = this.scheduleAdapter.adapt(data);
          return this.schedule;
        })
      );
  }

  getMyprofile(){
    return this.http.get(
      `${environment.apiUrl}/api/drivers/profile`
    ).pipe(map((res: any) => {
      this.driverProfile = this.driverAdapter.adapt(res);
      return this.driverProfile;
    }))
  }

  getAllReview(driverId: string){
    this.reviewList = [];
    return this.http.get(`${environment.apiUrl}/api/drivers/reviews/${driverId}`).pipe(
      map((data: any) => {
        console.log(data);
        data.rates.map((item) => {
          this.reviewList.push(this.reviewAdapter.adapt(item));
        });
        console.log(this.reviewList);
        return this.reviewList;
      })
    );
  }

  removeSchedule(driverId: string){
    return this.http.delete(
      `${environment.apiUrl}/api/drivers/schedules/${driverId}`
    )
  }

  removeVehicle(driverId: string){
    return this.http.delete(
      `${environment.apiUrl}/api/drivers/vehicles/${driverId}`
    )
  }

  makeReadNotify(notifyId: string){
    return this.http.put(
      `${environment.apiUrl}/api/notify/${notifyId}`,
      {}
    )
  }

}
