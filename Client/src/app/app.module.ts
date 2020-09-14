import { AppSettings } from '../AppSetting';
import { LoginComponent } from './modules/login/login.component';
import { LoginModule } from './modules/login/login.module';
import { TranslateService } from './core/i18n/translate.service';
import { TranslatePipe } from './core/i18n/translate.pipe';
import { JwtInterceptor } from './core/Interceptor/jwt.interceptor';
import { ReactiveFormsModule } from '@angular/forms';
import { RxReactiveFormsModule } from '@rxweb/reactive-form-validators';
import { DriverModule } from './modules/driver/driver.module';
import { ErrorInterceptor } from './core/Interceptor/error.interceptor';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppHeaderComponent } from './shared/components/app-header/app-header.component';
import { AppFooterComponent } from './shared/components/app-footer/app-footer.component';
import { SharedModule } from './shared/shared.module';
import { CoreModule } from './core/core.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { MatNativeDateModule } from '@angular/material/core';
import { RegisterAccountComponent } from './modules/register-account/register-account.component';

export function setupTranslateFactory(service: TranslateService): Function {
  return () => service.use(localStorage.getItem('language') || AppSettings.language);
}
@NgModule({
  declarations: [AppComponent, RegisterAccountComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    CoreModule,
    SharedModule,
    HttpClientModule,
    ReactiveFormsModule,
    RxReactiveFormsModule,
    MatNativeDateModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    TranslateService,
    {
      provide: APP_INITIALIZER,
      useFactory: setupTranslateFactory,
      deps: [TranslateService],
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
