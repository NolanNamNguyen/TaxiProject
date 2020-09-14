import { DialogComponent } from './../../../../shared/components/dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { tap } from 'rxjs/operators';
import { Vehicle } from './../../../../shared/models/vehicle';
import { DriverService } from './../../services/driver.service';
import { AuthService } from './../../../../core/auth/Auth.service';
import { Component, OnInit } from '@angular/core';
import { NgModule } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RxwebValidators, extension } from '@rxweb/reactive-form-validators';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { pipe, BehaviorSubject, Observable } from 'rxjs';
import { InformDialogComponent } from 'src/app/shared/components/inform-dialog/inform-dialog.component';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.scss'],
})
export class VehicleComponent implements OnInit {
  registerForm: FormGroup;
  imageUrl: any;
  selectedImg: File;
  myVehicle: Vehicle;


  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private route: ActivatedRoute,
    private auth: AuthService,
    private driverService: DriverService,
    private dialog: MatDialog

  ) {}

  ngOnInit() {
    const loadingDialog = this.dialog.open(InformDialogComponent, {
      width: '380px',
      height: '250px',
      disableClose: true,
      data: {
        isLoading: true,
        loadingMessage: 'Loading data...',
      },
    });
    this.getMyVehicle().subscribe((res) => {
      loadingDialog.close();
      this.buildForm();
    },
    (error) => {
      loadingDialog.close();
      this.buildForm();
    }
    );
  }

  buildForm() {
    this.registerForm = this.formBuilder.group({
      license: ['', Validators.required],
      vehiclename: ['', Validators.required],
      seater: ['', Validators.required],
    });
  }
  get vehicleForm() {
    return this.registerForm.controls;
  }

  onSubmit() {
    const formData = new FormData();
    formData.append('license', this.registerForm.get('license').value);
    formData.append('vehiclename', this.registerForm.get('vehiclename').value);
    formData.append('seater', this.registerForm.get('seater').value);
    formData.append('driverid', this.auth.getDriverId().toString());
    formData.append('image', this.selectedImg);
    this.driverService.registerVehicle(formData).subscribe(
      (response) => {
        console.log(response);
        this.ngOnInit();
      },
      (error) => {
        console.log(error);
      }
    );
    
  }
  onFileChange(event) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]); // read file as data url
      this.selectedImg = event.target.files[0];
      reader.onload = (event) => {
        // called once readAsDataURL is completed
        this.imageUrl = event.target.result;
      };
    }
    console.log(this.registerForm);
  }

  goBack() {
    this.router.navigate(['/driver']);
  }

  getMyVehicle() {
    return this.driverService
      .getVehicle(this.auth.getDriverId().toString())
      .pipe(
        tap(
          (vehicle) => {
            this.myVehicle = vehicle;
            
            console.log(vehicle);
          },
          (error) => {
            console.log(error);
          }
        )
      );
  }

  removeVehicle(){
    const deletingDialog = this.dialog.open(DialogComponent, {
      width: '380px',
      height: '180px',
      data: {
        isDeleting: true,
      },
    });
    deletingDialog.afterClosed().subscribe(
      (resdata) => {
        if(resdata){
          const loadingDialog = this.dialog.open(InformDialogComponent, {
            width: '380px',
            height: '180px',
            data: {
              isLoading: true,
              loadingMessage: "deleting the vehicle"
            },
          });
          this.driverService.removeVehicle(this.auth.getDriverId().toString()).subscribe(
            (resdata) => {
              loadingDialog.close();
              const successDialog = this.dialog.open(InformDialogComponent, {
                width: '380px',
                height: '200px',
                data: {
                  isSuccess: true,
                  successMessage: "Remove completed!"
                },
              });
              this.ngOnInit();
              console.log(resdata);
            },
            (error) => {
              loadingDialog.close();
              console.log(error);
            }
          )
        }
      }
    )
  }

}
