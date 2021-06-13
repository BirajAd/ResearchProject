import { Component, Input, OnInit } from '@angular/core';
import { Message } from 'src/app/_models/message';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-connection-messages',
  templateUrl: './connection-messages.component.html',
  styleUrls: ['./connection-messages.component.css']
})
export class ConnectionMessagesComponent implements OnInit {
  @Input() username:string;
  messages: Message[];

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages() {
    this.userService.getMessageThread(this.username).subscribe(messages => {
      this.messages =  messages;
    })
  }

}
