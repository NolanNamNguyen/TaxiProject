import { TranslateService } from './core/i18n/translate.service';
import { User } from './shared/models/user';
import { AuthService } from './core/auth/Auth.service';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import {
  Component,
  OnInit,
} from '@angular/core';
import * as $ from'jquery';
import { Observable, merge } from 'rxjs';
import { tap, startWith, delay, skip, filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'TaxiUmbrellaClient';
  header = false;
  footer = false;
  url: string;
  currentUser: User;

  constructor(
    private router: Router,
    private auth: AuthService,
    private translate: TranslateService
  ) {
    this.auth.currentUser.subscribe(x => this.currentUser = x)
  }
  ngOnInit() {
    this.router.events
      .pipe(
        filter((event: any) => event instanceof NavigationEnd),
        map((event: NavigationEnd) => event.url),
        tap((url: string) => this.toggleHeaderFooter(url))
      )
      .subscribe();
  }

  setLang(lang: string){
    this.translate.use(lang);
  }

  toggleInvi(){
    $(".listLanguage_container").toggleClass('listLanguage_container_reveal');
  }

  toggleHeaderFooter(url: string) {
    const hideHeaderUrlList = ['/admin'];
    const trimmedUrl = 
      url[url.length - 1] === '/' ? url.substring(0, url.length - 1) : url;
    // /a/b/c => split('/') => ['','a','b','c']
    if (
      hideHeaderUrlList.includes(trimmedUrl) ||
      trimmedUrl.split('/').length > 2
    ) {
      this.header = false;
      this.footer = false;
    } else {
      this.header = true;
      this.footer = true;
    }
  }
}
