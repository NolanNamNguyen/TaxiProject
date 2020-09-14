import { Adapter } from './../services/Adapter';
import { Order } from './order';
import { User } from './user';
import { Injectable } from '@angular/core';

export class Customer {
  constructor(
    public customerId: number,
    public userId: number,
    public userName: string,
    public name: string,
    public phone: string,
    public email: string,
    public address: string,
    public imagePath: string
  ) {}
}

@Injectable({
  providedIn: 'root',
})
export class CustomerAdapter implements Adapter<Customer> {
  adapt(item: any): Customer {
    return new Customer(
      item.customerId,
      item.userId,
      item.userName,
      item.name,
      item.phone,
      item.email,
      item.address,
      item.imagePath,
    );
  }
}
