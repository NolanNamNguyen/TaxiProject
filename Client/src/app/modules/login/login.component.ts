import { TranslateService } from './../../core/i18n/translate.service';
import { MatDialog } from '@angular/material/dialog';
import { AuthService } from '../../core/auth/Auth.service';
import { Component, OnInit, AfterViewInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import * as $ from 'jquery';
import { ActivatedRoute, Router } from '@angular/router';
import { InformDialogComponent } from 'src/app/shared/components/inform-dialog/inform-dialog.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  error = '';

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthService,
    public dialog: MatDialog,
    private translate: TranslateService
  ) {
    // redirect to home if already logged in
    if (this.authenticationService.currentUserValue) {
      // this.router.navigate(['admin'],{relativeTo: route});
    }
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });

    // get return url from route parameters or default to '/'
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.loginForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      console.log('fail');
      return ;
    }
    const loadingDialog = this.dialog.open(InformDialogComponent, {
      width: '380px',
      height: '230px',
      data: {
        isLoading: true,
        isSuccess: false,
        isFailed: false,
        loadingMessage: "Logging in..."
      },
    });
    this.authenticationService
      .login(this.f.username.value, this.f.password.value)
      .pipe()
      .subscribe(
        (data) => {
          loadingDialog.close();
          this.router.navigate([this.authenticationService.getRole()],{relativeTo: this.route});
          console.log(data);
        },
        (error) => {
          loadingDialog.close();
          const errorDialog = this.dialog.open(InformDialogComponent, {
            width: '380px',
            height: '180px',
            data: {
              isLoading: false,
              isSuccess: false,
              isFailed: true,
              failedMessage: "Please check your username and password"
            },
          });
          console.log(error);
          this.error = error;
        }
      );
  }


  
  logout(){
    this.authenticationService.logout();
  }
}
