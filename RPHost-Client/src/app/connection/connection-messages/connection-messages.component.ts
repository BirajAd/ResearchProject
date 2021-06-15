import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Message } from 'src/app/_models/message';
import { User } from 'src/app/_models/user';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-connection-messages',
  templateUrl: './connection-messages.component.html',
  styleUrls: ['./connection-messages.component.css']
})
export class ConnectionMessagesComponent implements OnInit, OnChanges {
  @Input() username:string;
  messages: Message[];
  user: User;

  constructor(private userService: UserService, private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.user = this.authService.currentUser;
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    // console.log(changes);
    // this.username = changes.username.currentValue;
    this.loadMessages();
    this.route.data.subscribe(data => {
      this.user = this.authService.currentUser;
    });
    console.log(this.user.username);
  }

  loadMessages() {
    this.userService.getMessageThread(this.username).subscribe(messages => {
      this.messages =  messages;
    })
  }

}
