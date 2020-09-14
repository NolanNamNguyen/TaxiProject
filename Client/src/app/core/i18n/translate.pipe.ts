import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from './translate.service';

@Pipe({
  name: 'translate',
  pure: false,
})
export class TranslatePipe implements PipeTransform {
  constructor(private translate: TranslateService) {}
  transform(keys: any): any {
    if(keys.includes(".")){
        keys = keys.split(".");
    }
    let obj = this.translate.data;
    return keys.reduce((obj, key) => {
      return typeof obj !== 'undefined' ? obj[key] : "something wrong";
    }, obj);
  }
}
