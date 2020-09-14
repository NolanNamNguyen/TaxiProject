import { InformDialogComponent } from './../../shared/components/inform-dialog/inform-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { containsValidatorExtension } from '@rxweb/reactive-form-validators/validators-extension';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { Router } from '@angular/router';
import { AuthService } from './../../core/auth/Auth.service';
import { Component, OnInit } from '@angular/core';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-register-account',
  templateUrl: './register-account.component.html',
  styleUrls: ['./register-account.component.scss']
})
export class RegisterAccountComponent implements OnInit {

  registerForm: FormGroup;
  submitted = false;
  constructor(
    private formBuilder: FormBuilder,
    private authenticationService: AuthService,
    private router: Router,
    public dialog: MatDialog,

  ) { }

  ngOnInit(): void {
    this.submitted = false
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
      name: ['', Validators.required],
      phone: [''],
      email: ['', [Validators.required, Validators.email]],
      address: [''],
      role: ['', Validators.required],
    },{
      validators: this.MustMatch('password', 'confirmPassword')
    });
  }

  MustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
        const control = formGroup.controls[controlName];
        const matchingControl = formGroup.controls[matchingControlName];

        if (matchingControl.errors && !matchingControl.errors.mustMatch) {
            // return if another validator has already found an error on the matchingControl
            return;
        }

        // set error on matchingControl if validation fails
        if (control.value !== matchingControl.value) {
            matchingControl.setErrors({ mustMatch: true });
        } else {
            matchingControl.setErrors(null);
        }
    }
}
  
  get regisControl(){
    return this.registerForm.controls;
  }

  onSubmit(){
    this.submitted = true;
    if (this.registerForm.invalid) {
      console.log('fail');
      return ;
    }

    const loadingDialog = this.dialog.open(InformDialogComponent, {
      width: '380px',
      height: '250px',
      disableClose: true,
      data: {
        isLoading: true,
        loadingMessage: 'Loding Data...',
      },
    });

    let formbody ={
      username: this.regisControl.username.value,
      password: this.regisControl.password.value,
      confirmPassword: this.regisControl.confirmPassword.value,
      name: this.regisControl.name.value,
      phone: this.regisControl.phone.value,
      email: this.regisControl.email.value,
      address: this.regisControl.address.value,
      role: this.regisControl.role.value
    }
    this.authenticationService
      .register(formbody)
      .pipe()
      .subscribe(
        (data) => {
          loadingDialog.close();
          const successDialog = this.dialog.open(InformDialogComponent, {
            width: '380px',
            height: '230px',
            data: {
              isSuccess: true,
              successMessage: 'successful register',
            },
          });
          setTimeout(() => {
            this.router.navigate(['']);
          }, 1000);
          console.log(data);
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
          console.log(error);
        },()=>{
          
        }
      );
  }

  getcomboBoxValue(){
    // console.log(this.regisControl.role.value);
  }

}
