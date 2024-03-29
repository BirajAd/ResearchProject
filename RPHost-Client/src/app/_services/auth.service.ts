import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
// import {BehaviorSubject} from 'rxjs';
import {map} from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { BehaviorSubject } from 'rxjs';
import { PresenceService } from './presence.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;
  userForMessage = new BehaviorSubject<string>('');
  messageUser = this.userForMessage.asObservable();
  photoPath = new BehaviorSubject<string>('../../assets/user.png');
  currentPhotoPath = this.photoPath.asObservable();
  // photoPath = new BehaviorSubject<string>('../../assets/user.png');
  // currentPhotoPath = this.photoPath.asObservable();

constructor(private http: HttpClient, private presence: PresenceService) { }

changeUserPhoto(photoPath: string) {
  this.photoPath.next(photoPath);
}

login(model: any) {
  return this.http.post(this.baseUrl + 'login', model)
  .pipe(
    map((response: any) => {
      const user = response;
      if (user) {
        localStorage.setItem('token', user.token);
        localStorage.setItem('user', JSON.stringify(user.user));
        this.decodedToken = this.jwtHelper.decodeToken(user.token);
        this.currentUser = user.user;
        this.presence.createHubConnection(this.currentUser);
        this.changeUserPhoto(this.currentUser.photoPath);
        localStorage.setItem('profile', this.currentUser.photoPath);
      }
    })
  );
}

register(user: User){
  return this.http.post(this.baseUrl + 'register', user);
}

// register(model:any) {
//   return this.http.post(this.baseUrl + 'register', model).pipe(
//     map((response: any) => {
//       const user = response;
//       if (user) {
//         localStorage.setItem('token', user.token);
//         localStorage.setItem('user', JSON.stringify(user.user));
//         this.currentUser = user.user;
//         this.presence.createHubConnection(this.currentUser);
//         this.changeUserPhoto(this.currentUser.photoPath);
//         localStorage.setItem('profile', this.currentUser.photoPath);
//       }
//     })
//   )
// }

loggedIn() {
  const token = localStorage.getItem('token');
  return !this.jwtHelper.isTokenExpired(token);
}

}
