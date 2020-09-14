import { AppSettings } from '../../AppSetting';
import { TranslatePipe } from './i18n/translate.pipe';
import { TranslateService } from './i18n/translate.service';
import { JwtInterceptor } from './Interceptor/jwt.interceptor';
import { ErrorInterceptor } from './Interceptor/error.interceptor';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from './auth/Auth.service';

@NgModule({
  declarations: [],
  imports: [CommonModule],
  providers: [
    AuthService,
    JwtInterceptor
  ],
})
export class CoreModule {}
