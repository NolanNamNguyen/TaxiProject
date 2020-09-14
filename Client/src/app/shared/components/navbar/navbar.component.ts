import { Notify } from './../../models/notify';
import { RouteInfo } from './../../models/routeinfo';
import { AuthService } from './../../../core/auth/Auth.service';
import { Component, OnInit, ElementRef, Input, Output, EventEmitter } from '@angular/core';
import * as $ from 'jquery';
import {
  Location,
  LocationStrategy,
  PathLocationStrategy,
} from '@angular/common';
import { Router } from '@angular/router';
import { inject } from '@angular/core/testing';
import { not } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  @Input() notiList: Notify[];
  @Input() taxiAppRoute: RouteInfo[];
  @Input() unReadNotiList: Notify[];
  @Output() makeReadNoti: EventEmitter<Notify> = new EventEmitter<Notify>();
  private listTitles: any[];
  constructor(
    private authService: AuthService,
    private router: Router,
    private location: Location
  ) {}

  ngOnInit() {
    this.listTitles = this.taxiAppRoute.filter((listTitle) => listTitle);
    console.log(this.notiList);
  }

  logout() {
    this.router.navigate(['']);
    this.authService.logout();
  }

  getTitle() {
    var titlee = this.location.prepareExternalUrl(this.location.path());
    if (titlee.charAt(0) === '/') {
      titlee = titlee.slice(titlee.lastIndexOf('/'));
    }

    for (var item = 0; item < this.listTitles.length; item++) {
      if (this.listTitles[item].path === titlee) {
        return this.listTitles[item].title;
      }
    }
    return 'Dashboard';
  }
  makeVisible() {
    $('.notifyContainer').toggleClass('inviNotify');
  }

  makeAsRead(hoverNoti: Notify){
    if(!hoverNoti.isRead){
      this.makeReadNoti.emit(hoverNoti);
    }
  }
}
