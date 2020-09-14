import { AuthService } from './../../core/auth/Auth.service';
import { RouteInfo } from './../../shared/models/routeinfo';
import { AdminServiceService } from './services/admin.service';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';



@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  userName: string;
  userRole: string;
  myRoute: RouteInfo[];

  constructor(private adminService: AdminServiceService, private router: Router, private route: ActivatedRoute,private authService: AuthService ) {
    this.userName = this.authService.getUserName();
    this.userRole = this.authService.getRole();
   }

  ngOnInit(): void {
     this.myRoute = [
      { path: '/dashboard', title: 'Dashboard',  icon: 'dashboard', class: '' },
      { path: '/user_list', title: 'Customer List',  icon:'person', class: '' },
      { path: '/driver_list', title: 'Driver List',  icon:'content_paste', class: '' },
    ];
    this.router.navigate(['dashboard'],{relativeTo: this.route});
  }
  getUser(){
  }
}
