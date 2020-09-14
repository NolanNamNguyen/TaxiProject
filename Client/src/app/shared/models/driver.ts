import { Injectable } from '@angular/core';
import { Adapter } from './../services/Adapter';

export class Driver {
  constructor(
    public driverId: number,
    public userId: number,
    public userName: string,
    public name: string,
    public phone: string,
    public email: string,
    public address: string,
    public imagePath: string,
    public rateAverage: number,
  ) {}
}

@Injectable({
  providedIn: 'root',
})
export class DriverAdapter implements Adapter<Driver> {
  adapt(item: any): Driver {
    return new Driver(
      item.driverId,
      item.userId,
      item.userName,
      item.name,
      item.phone,
      item.email,
      item.address,
      item.imagePath,
      item.rateAverage
    );
  }
}
