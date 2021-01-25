import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { User } from './_models/user';
// import { FlexLayout } from '@angular/flex-layout';
import { MediaObserver, MediaChange } from '@angular/flex-layout';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  jwtHelper = new JwtHelperService();
  modalRef: BsModalRef;
  scrnSize: string;
  media$: Observable<MediaChange[]>;

  constructor(private authService: AuthService, private modalService: BsModalService, media: MediaObserver)
  {
      this.media$ = media.asObservable();
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, { backdrop: 'static', keyboard: false });
  }

  ngOnInit() {
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user'));

    this.media$.subscribe(mq => {
      console.log(mq[0].mqAlias);
      this.scrnSize = mq[0].mqAlias;
    });

    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }

    if (user) {
      this.authService.currentUser = user;
   
      this.authService.changeUserPhoto(user.photoPath);
    
    }
  }
}
