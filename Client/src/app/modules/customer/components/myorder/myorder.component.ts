import { Order } from './../../../../shared/models/order';
import { AuthService } from './../../../../core/auth/Auth.service';
import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { CustomerService } from '../../services/customer.service';
import { Schedule } from 'src/app/shared/models/schedule';
import { concatMap, tap } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
  selector: 'app-myorder',
  templateUrl: './myorder.component.html',
  styleUrls: ['./myorder.component.scss'],
})
export class MyorderComponent implements OnInit, OnDestroy {
  @Input() driverImg: string;
  mySchedule: Schedule;
  myOrder: Order;

  constructor(
    private cusService: CustomerService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    // this.getOrderList()
    //   .pipe(concatMap((orderList) => this.getCurrentOrder()))
    //   .subscribe();
    this.getCurrentOrder().subscribe();
  }

  getCurrentOrder() {
    return this.cusService
      .getCurrentOrder(this.authService.getCustomerId().toString())
      .pipe(
        tap((currentOrder) => {
          this.myOrder = currentOrder;
        })
      );
  }

  getOrderList() {
    return this.cusService.getAllOrders();
  }

  ngOnDestroy() {}

  removeCurrentOrder() {
    this.cusService
      .removeOrder(this.myOrder.orderId)
      .subscribe((response) => {
        
      });
    this.myOrder = null;
  }
}
