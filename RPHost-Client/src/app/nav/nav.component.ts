import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router, ActivatedRoute } from '@angular/router';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  user: User;
  model: any = {};

  // tslint:disable-next-line: max-line-length
  constructor(private userService: UserService, public authService: AuthService, private alertify: AlertifyService,
              private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    // this.loadUser();
  }

  // loadUser() {
  //   this.userService.getUser(this.authService.decodedToken.nameid).subscribe((user: User) => {
  //       this.user = user;
  //   }, error => {
  //     this.alertify.error(error);
  //   });
  // }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in successfully');
    }, error => {
      this.alertify.error('Failed to login');
    }, () => {
      this.router.navigate(['/researches']);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message('logged out.');
    this.router.navigate(['/home']);
  }

}
