import { RouteInfo } from './../../../../shared/models/routeinfo';
import { Component, OnInit, Input } from '@angular/core';



@Component({
  selector: 'app-driver-sidebar',
  templateUrl: './driver-sidebar.component.html',
  styleUrls: ['./driver-sidebar.component.scss']
})
export class DriverSidebarComponent implements OnInit {

  @Input() role: string;
  @Input() userName: string;
  @Input() driverRoute: RouteInfo[];
  
  constructor() {
    
   }

  ngOnInit(): void {
  }

}
