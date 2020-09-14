import { Adapter } from './../services/Adapter';
import { Injectable } from '@angular/core';

export class Vehicle {
  constructor(
    public vehicleId: number,
    public license: string,
    public seater: number,
    public vehicleName: string,
    public imagePath: string,

  ) {}
}

@Injectable({
  providedIn: 'root',
})
export class VehicleAdapter implements Adapter<Vehicle> {
  adapt(item: any): Vehicle {
    return new Vehicle(
      item.vehicleId,
      item.license,
      item.seater,
      item.vehicleName,
      item.imagePath
    );
  }
}
