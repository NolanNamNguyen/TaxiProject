import { RouteInfo } from './../../../shared/models/routeinfo';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss'],
})
export class SideBarComponent implements OnInit {
  @Input() taxiAppRoute: RouteInfo[];
  @Input() role: string;
  @Input() userName: string;
  constructor() {

  }

  ngOnInit(): void {}
}
