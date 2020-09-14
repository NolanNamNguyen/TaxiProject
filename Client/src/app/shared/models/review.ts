import { Injectable } from '@angular/core';
import { Adapter } from './../services/Adapter';
import { User } from './user';

export class Review {
  constructor(
    public rate: number,
    public rateContent: string,
    public customerId: string,
    public customerName: string,
    public customerImage: string,
    public createdDate: Date,
  ) {}
}

@Injectable({
  providedIn: 'root',
})
export class ReviewAdapter implements Adapter<Review> {
  adapt(item: any): Review {
    return new Review(
      item.rate,
      item.rateContent,
      item.customerId,
      item.customerName,
      item.customerImage,
      item.created,
    );
  }
}
