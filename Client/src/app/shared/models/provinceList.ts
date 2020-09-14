import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class Province {
  public provinceList: Object[] = [
    { provinceKey: 1, provinceName: 'Thanh Hoá' },
    { provinceKey: 2, provinceName: 'Nghệ An' },
    { provinceKey: 3, provinceName: 'Hà Tĩnh' },
    { provinceKey: 4, provinceName: 'Quảng Bình' },
    { provinceKey: 5, provinceName: 'Quảng Trị' },
    { provinceKey: 6, provinceName: 'Thừa Thiên Huế' },
    { provinceKey: 7, provinceName: 'Đà Nẵng' },
    { provinceKey: 8, provinceName: 'Quảng Nam' },
    { provinceKey: 9, provinceName: 'Quãng Ngãi' },
    { provinceKey: 10, provinceName: 'Bình Định' },
    { provinceKey: 11, provinceName: 'Phú Yên' },
  ];
  getProvinceList(): Object[]{
    return this.provinceList;
  }
}
