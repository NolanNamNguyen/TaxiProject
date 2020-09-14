import { DriverService } from './services/driver.service';
import { Notify } from './../../shared/models/notify';
import { environment } from './../../../environments/environment';
import { RouteInfo } from './../../shared/models/routeinfo';
import { AuthService } from './../../core/auth/Auth.service';
import { Component, OnInit } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Router, ActivatedRoute } from '@angular/router';
import * as _signalR from '@aspnet/signalr';

export const myROUTES: RouteInfo[] = [
  { path: '/profile_page', title: 'Profile', icon: 'person', class: '' },
  { path: '/vehicle', title: 'Vehicle', icon: 'directions_car', class: '' },
  { path: '/schedule', title: 'Schedule', icon: 'content_paste', class: '' },
  {
    path: '/order_list',
    title: 'Order List',
    icon: 'library_books',
    class: '',
  },
];
@Component({
  selector: 'app-driver',
  templateUrl: './driver.component.html',
  styleUrls: ['./driver.component.scss'],
})
export class DriverComponent implements OnInit {
  userName: string;
  userRole: string;
  userId: string;
  customerName: string;
  startProvince: string;
  desProvince: string;
  mess: string;
  notifyList: Notify[];
  unReadNotiList: Notify[];

  private _connection: _signalR.HubConnection;

  driverRoute: RouteInfo[];

  constructor(
    private authService: AuthService,
    private notifier: NotifierService,
    private router: Router,
    private route: ActivatedRoute,
    private driverService: DriverService
  ) {
    this.userRole = this.authService.getRole();
    this.driverRoute = myROUTES.filter((route) => route);
    // signalR implement
    this.userId = this.authService.getUserId();
    this.userRole = this.authService.getRole();
    this.userName = this.authService.getUserName();
    // signalR implement
  }

  ngOnInit(): void {
    this.getMyNotifyList();
    this.driverService
      .getVehicle(this.authService.getDriverId().toString())
      .subscribe(
        (res) => {},
        (error) => {
          console.log(error);
        }
      );
    this.notifyList = [];
    this.unReadNotiList = [];
    this._connection = new _signalR.HubConnectionBuilder()
      .withUrl(
        `${environment.apiUrl}/Notifications?userid=${this.userId}&role=${this.userRole}&username=${this.userName}`
      )
      .build();

    this._connection
      .start()
      .then(() => console.log('Connetion started'))
      .catch((err) => console.log(err));

    this._connection.on('SendBookingInfoToDriver', (order) => {
      this.customerName = order.customerName;
      this.startProvince = order.pickupLocation;
      this.desProvince = order.returnLocation;
      this.mess =
        'Có Order mới: Customer: ' +
        this.customerName +
        '; Nơi book: ' +
        this.startProvince +
        '; Nơi đến: ' +
        this.desProvince;
      this.showNotification('success', this.mess);
      console.log(order);
      this.getMyNotifyList();
    });

    this.router.navigate(['profile_page'], { relativeTo: this.route });
  }

  getMyNotifyList() {
    this.unReadNotiList = [];
    this.authService.getAllNotify().subscribe(
      (resdata) => {
        console.log('asodfjioasdjfoijsafd');
        console.log(resdata);
        this.notifyList = resdata;
        if (this.notifyList) {
          this.unReadNotiList = this.notifyList.filter((noti) => noti.isRead == false);
        }
      },
      (error) => {
        console.log(error);
      }
    );
  }

  showNotification(type: string, message: string): void {
    console.log('oklah');
    this.notifier.notify(type, message);
  }

  makeAsRead(event){
    var myNotify: Notify = event;
    this.driverService.makeReadNotify(myNotify.notifyId.toString()).subscribe(
      (res) => {
        console.log(res);
        this.getMyNotifyList();
      },
      (error) => {
        console.log(error);
      }
    )
  }

}
