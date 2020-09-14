import { ROLE } from './../../shared/models/role';
import { AuthGuard } from './../../core/auth/auth.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  {
    path: 'admin',
    loadChildren: () =>
      import('../admin/admin.module').then((m) => m.AdminModule),
    canActivate: [AuthGuard],
    data: {
      role: ROLE.ADMIN,
    },
  },
  {
    path: 'driver',
    loadChildren: () =>
      import('../driver/driver.module').then((m) => m.DriverModule),
    canActivate: [AuthGuard],
    data: {
      role: ROLE.DRIVER,
    },
  },
  {
    path: 'customer',
    loadChildren: () =>
      import('../customer/customer.module').then((m) => m.CustomerModule),
      canActivate: [AuthGuard],
      data: {
        role: ROLE.CUSTOMER,
      },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LoginRoutingModule {}
