import { InformDialogComponent } from 'src/app/shared/components/inform-dialog/inform-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { Customer } from 'src/app/shared/models/customer';
import { DriverService } from './../../services/driver.service';
import { environment } from './../../../../../environments/environment';
import { AuthService } from './../../../../core/auth/Auth.service';
import { Component, OnInit, Input } from '@angular/core';
import * as $ from 'jquery';
import { NotifierService } from 'angular-notifier';
import { Driver } from 'src/app/shared/models/driver';
import { Review } from 'src/app/shared/models/review';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss'],
})
export class ProfilePageComponent implements OnInit {
  myProfile: Driver;
  customerReview: Customer;
  reviewList: Review[];
  @Input() userName: string;
  classlist: string[] = ['about_container', 'comment_bigContainer'];

  CustomerName: string;
  userId: string;
  userRole: string;

  constructor(
    private authService: AuthService,
    private driverService: DriverService,
    public dialog: MatDialog,

  ) {}

  ngOnInit(): void {
    this.getMyprofile();
    this.getMyReviews();
  }

  getMyprofile() {
    const loadingDialog = this.dialog.open(InformDialogComponent, {
      width: '380px',
      height: '250px',
      disableClose: true,
      data: {
        isLoading: true,
        loadingMessage: 'Loding Data...',
      },
    });
    this.driverService.getMyprofile().subscribe(
      (resdata) => {
        loadingDialog.close();
        this.myProfile = resdata;
        if ((this.myProfile.rateAverage.toString() == "NaN")) {
          this.myProfile.rateAverage = 0;
        }
      },
      (error) => {
        loadingDialog.close();
        const errorDialog = this.dialog.open(InformDialogComponent, {
          width: '380px',
          height: '180px',
          data: {
            isFailed: true,
            failedMessage: 'Something wrong happened',
          },
        });
        this.myProfile = null;
        console.log(error);
      }
    );
  }

  getMyReviews() {
    this.driverService
      .getAllReview(this.authService.getDriverId().toString()).subscribe(
        (resdata) => {
          // console.log("abc ok");
          // console.log(resdata);
          this.reviewList = resdata;
        },
        (error) => {
          this.reviewList = null;
          console.log(error);
        }
      );
  }
}
