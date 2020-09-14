import { Injectable } from '@angular/core';
import { Component, OnInit, Inject } from '@angular/core';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import * as $ from 'jquery';
import { dataUri } from '@rxweb/reactive-form-validators';

export interface InformData {
  isLoading: boolean;
  isSuccess: boolean;
  isFailed: boolean;
  loadingMessage: string;
  isAlreadyRate: boolean;
  successMessage: string;
  failedMessage: string;
}


@Component({
  selector: 'app-inform-dialog',
  templateUrl: './inform-dialog.component.html',
  styleUrls: ['./inform-dialog.component.scss'],
})
export class InformDialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<InformDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: InformData
  ) {}

  ngOnInit() {
    
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
  onYesClick(): boolean {
    return true;
  }

 
}
