import { NotifyAdapter } from './../../shared/models/notify';
import { User } from '../../shared/models/user';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Notify } from '../../shared/models/notify'
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  isLogin = false;
  notifyList: Notify[];
  localUser: any;
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient, private notiAdapter: NotifyAdapter) {
    this.currentUserSubject = new BehaviorSubject<User>(
      JSON.parse(localStorage.getItem('currentUser'))
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
}

  login(username: string, password: string) {
    let body = {
      userName: username,password: password
    };
    return this.http.post<any>(`${environment.apiUrl}/api/users/authenticate`,body)
    .pipe(map((user) => {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        console.log(user);
        return user;
      })
    );
  }

  getAllNotify(){
    this.notifyList = [];
    return this.http.get(
      `${environment.apiUrl}/api/notify`
    ).pipe(map((resdata: any[]) => {
      resdata.map(item => {
        this.notifyList.push(this.notiAdapter.adapt(item));
      })
      return this.notifyList;
    }))
  }

  register(formbody: any){
    return this.http.post<any>(`${environment.apiUrl}/api/users/register`,formbody)
    .pipe(map((data)=>{
      console.log(data)
      return data;
    }))
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  isLoggedIn() {
    const loggedIn = localStorage.getItem('currentUser');
    if (loggedIn) this.isLogin = true;
    else this.isLogin = false;
    return this.isLogin;
  }

  getDriverId(): number{
    let user = this.currentUserValue;
    if(user.driverId){
      return user.driverId;
    }
    return null
  }

  getCustomerId(): number{
    let user = this.currentUserValue;
    if(user.customerId){
      return user.customerId;
    }
    return null
  }

  getUserName(){
    return this.currentUserValue.name;
  }

  getRole() {
    return this.currentUserValue.role.toLowerCase();
  }

  getUserId(){
    return this.currentUserValue.userId.toString();
  }
}
