import { Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
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
  @ViewChild('messageForm') messageForm: NgForm;
  @Input() username:string;
  messages: Message[];
  user: User;
  messageContent: string;

  constructor(private userService: UserService, private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges) {
    this.loadMessages();
    this.route.data.subscribe(data => {
      this.user = this.authService.currentUser;
    });
  }

  loadMessages() {
    this.userService.getMessageThread(this.username).subscribe(messages => {
      this.messages =  messages;
    })
  }

  sendMessage() {
    this.userService.sendMessage(this.username, this.messageContent).subscribe(message => {
      this.messages.push(message);
      this.messageForm.reset();
    })
  }

}
