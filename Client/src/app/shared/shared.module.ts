import { TranslateService } from './../core/i18n/translate.service';
import { AppSettings } from './../../AppSetting';
import { FormsModule } from '@angular/forms';
import { customNotifierOptions } from './models/notifierOptions';
import { NavbarComponent } from './components/navbar/navbar.component';
import { VehicleAdapter } from './models/vehicle';
import { RouterModule } from '@angular/router';
import { AppFooterComponent } from './components/app-footer/app-footer.component';
import { AppHeaderComponent } from './components/app-header/app-header.component';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppLogoComponent } from './components/app-logo/app-logo.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { DialogComponent } from './components/dialog/dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { NotifierModule } from 'angular-notifier';
import { InformDialogComponent } from './components/inform-dialog/inform-dialog.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { TranslatePipe } from '../core/i18n/translate.pipe';


@NgModule({
  declarations: [
    AppLogoComponent,
    AppHeaderComponent,
    AppFooterComponent,
    NavbarComponent,
    DialogComponent,
    InformDialogComponent,
    TranslatePipe,
  ],
  imports: [
    CommonModule,
    RouterModule,
    MatDialogModule,
    NotifierModule.withConfig(customNotifierOptions),
    FormsModule,
    MatProgressSpinnerModule
  ],
  exports: [
    AppLogoComponent,
    AppHeaderComponent,
    AppFooterComponent,
    NavbarComponent,
    MatSortModule,
    MatTableModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatDialogModule,
    NotifierModule,
    TranslatePipe
  ],
  providers: [

  ]
})
export class SharedModule {}
