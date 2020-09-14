import { Adapter } from './../services/Adapter';
import { Injectable } from "@angular/core";

export class User {
  address: string;
  created: Date;
  driverId: number;
  customerId: number
  email: string;
  imagePath: string;
  isVerified: boolean;
  name: string;
  phone: string;
  role: string;
  token: string;
  userId: number;
  userName: string;
  constructor() {}
}

@Injectable({
  providedIn: "root",
})
export class UserAdapter implements Adapter<User> {
  adapt(item: any): User {
    return new User();
  }
}