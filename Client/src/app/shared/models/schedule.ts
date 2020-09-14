import { Adapter } from './../services/Adapter';
import { Injectable } from '@angular/core';

export class Schedule {
  constructor(
    public scheduleId: number,
    public startAddress: string,
    public startProvince: string,
    public destinationAddress: string,
    public destinationProvince: string,
    public imagePath: any,
    public driverName: string,
    public driverPhone: string,
    public rateAverage: number,
    public vehicleName: string,
    public availableSlot: number,
    public startTime: Date,
    public driverId: number
  ) {}
}

@Injectable({
  providedIn: 'root',
})
export class ScheduleAdapter implements Adapter<Schedule> {
  adapt(item: any): Schedule {
    return new Schedule(
      item.scheduleId,
      item.startAddress,
      item.startProvince,
      item.destinationAddress,
      item.destinationProvince,
      item.driverImage,
      item.driverName,
      item.driverPhone,
      item.rateAverage,
      item.vehicleName,
      item.availablableSlot,
      item.startTime,
      item.driverId
    );
  }
}
