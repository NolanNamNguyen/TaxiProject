import { Router, ActivatedRoute } from '@angular/router';
import { RouteInfo } from './../../shared/models/routeinfo';
import { AuthService } from './../../core/auth/Auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss'],
})
export class CustomerComponent implements OnInit {
  userName: string;
  userRole: string;
  customerRoute: RouteInfo[];
  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute) {
    this.userName = this.authService.getUserName();
    this.userRole = this.authService.getRole();
  }

  ngOnInit(): void {
    this.customerRoute = [
      { path: '/profile_page', title: 'customerModule.profile',  icon:'person', class: '' },
      { path: '/seach_booking', title: 'customerModule.search',  icon: 'directions_car', class: '' },
      { path: '/my_order', title: 'customerModule.order',  icon:'assignment_turned_in', class: '' },
    ];
    this.router.navigate(['profile_page'],{relativeTo: this.route});
  }
}
