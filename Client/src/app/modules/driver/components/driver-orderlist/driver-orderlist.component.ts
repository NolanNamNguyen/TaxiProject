import { DriverService } from './../../services/driver.service';
import { Customer } from 'src/app/shared/models/customer';
import { Order } from '../../../../shared/models/order';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-driver-orderlist',
  templateUrl: './driver-orderlist.component.html',
  styleUrls: ['./driver-orderlist.component.scss']
})
export class DriverOrderlistComponent implements OnInit {
  myOrderList: Order[];
  orderedCustomer: Customer;

  constructor(private driverService: DriverService) { }

  ngOnInit(){
    this.getOrderList();
  }

  getOrderList(){
    this.driverService.getAllOrders().subscribe(list => this.myOrderList = list);
  }

  completeOrder(orderId: string){
    this.driverService.completeOrder(orderId).subscribe(
      (response) =>{
        console.log(response);
        this.getOrderList();
      },
      (error) => console.log(error)
    )
  }
}
