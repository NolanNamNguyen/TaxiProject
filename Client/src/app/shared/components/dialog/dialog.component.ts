import { Injectable } from '@angular/core';
import { Component, OnInit, Inject } from '@angular/core';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import * as $ from 'jquery';
import { dataUri } from '@rxweb/reactive-form-validators';

export interface DialogData {
  isLoading: boolean;
  isDeleting: boolean;
  confirmDelete: boolean;
  isReviewing: boolean;
  orderId: string;
  driverAvatar: string;
  driverName: string;
  rate: number;
  rateContent: string;
}


@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss'],
})
export class DialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<DialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData
  ) {}

  ngOnInit() {
    const self= this;
    $(function () {
      $('#rate').click(function(){
        $(this).unbind("mousemove");
      })
      $('#rate').mouseenter(function (){
        $('#rate').mousemove(function (event) {
          var relX = event.pageX - $(this).offset().left;
          let percentWidtOf5 = relX/$(this).width() * 5;
          let rateStarNum = Math.floor(percentWidtOf5) + 1 ;
          if(rateStarNum > 0 && rateStarNum < 6){
            self.data.rate = rateStarNum;
          }
        });
      })
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
  onYesClick(): boolean {
    return true;
  }

 
}
