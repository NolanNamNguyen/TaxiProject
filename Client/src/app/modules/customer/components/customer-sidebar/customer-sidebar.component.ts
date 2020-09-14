import { RouteInfo } from './../../../../shared/models/routeinfo';
import { Component, OnInit, Input } from '@angular/core';


@Component({
  selector: 'app-customer-sidebar',
  templateUrl: './customer-sidebar.component.html',
  styleUrls: ['./customer-sidebar.component.scss']
})
export class CustomerSidebarComponent implements OnInit {

  @Input() role: string;
  @Input() userName: string;
  @Input() customerRoute: RouteInfo[];
  
  
  constructor() {

  }

  ngOnInit(): void {
    
  }

}
