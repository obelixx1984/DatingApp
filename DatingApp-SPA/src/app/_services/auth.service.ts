import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {BehaviorSubject} from 'rxjs';
import {map} from 'rxjs/operators';
import {JwtHelperService} from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;
  zdjecieUrl = new BehaviorSubject<string>('../../assets/user.png');
  dowolneZdjecieUrl = this.zdjecieUrl.asObservable();

constructor(private http: HttpClient) { }

zmienZdjecieUzytkownika(zdjecieUrl: string) {
  this.zdjecieUrl.next(zdjecieUrl);
}

login(model: any) {
  return this.http.post(this.baseUrl + '/login', model)
    .pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', JSON.stringify(user.user));
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.currentUser = user.user;
          this.zmienZdjecieUzytkownika(this.currentUser.zdjecieUrl);
        }
      })
    );
}

register(model: any) {
  return this.http.post(this.baseUrl + '/register', model);
}

loggedIn() {
  const token = localStorage.getItem('token');
  return !this.jwtHelper.isTokenExpired(token);
}

}
