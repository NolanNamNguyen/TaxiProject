import { Adapter } from './../services/Adapter';
import { Driver } from './driver';
import { Customer } from './customer';
import { Injectable } from '@angular/core';
export class Order {
  constructor(
    public orderId: number,
    public pickUpLocation: string,
    public returnLocation: string,
    public status: boolean,
    public isCancel: boolean,
    public price: number,
    public rate: number,
    public rateContent: string,
    public created: Date,
    public driverId: number,
    public driverName: string,
    public driverImg: string,
    public driverRate: number,
    public customerId: string,
    public customerPhone: string,
    public customerName: string,
    public customerImg: string
  ) {}
 
}

@Injectable({
  providedIn: 'root',
})
export class OrderAdapter implements Adapter<Order> {
  adapt(item: any): Order {
    return new Order(
      item.orderId,
      item.pickupLocation,
      item.returnLocation,
      item.status,
      item.isCancel,
      item.price,
      item.rate,
      item.rateContent,
      item.created,
      item.driverId,
      item.driverName,
      item.driverImg,
      item.driverRate,
      item.customerId,
      item.customerPhone,
      item.customerName,
      item.customerImg
    );
  }
}
