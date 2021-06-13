import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-connection-card',
  templateUrl: './connection-card.component.html',
  styleUrls: ['./connection-card.component.css']
})

export class ConnectionCardComponent implements OnInit {
  @Input() user: User;

  constructor( private authService: AuthService, 
    private userService: UserService, private alertify: AlertifyService ){

  }

  ngOnInit() {
  }

  sendFollow(id: number) {
    this.userService.sendFollow(this.authService.decodedToken.nameid, id).subscribe(data => {
      this.alertify.success('You started following ' + this.user.username);
    }, error => {
      this.alertify.error("Already followed "+this.user.username);
    });
  }

}
