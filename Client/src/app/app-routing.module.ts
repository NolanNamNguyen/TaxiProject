import { LoginComponent } from './modules/login/login.component';
import { RegisterAccountComponent } from './modules/register-account/register-account.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./modules/login/login.module').then((m) => m.LoginModule),
  },
  // {
  //   path: '', component: LoginComponent
  // },
  {
    path: 'register', component: RegisterAccountComponent
  }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
