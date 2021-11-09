import { AfterViewChecked, Component, ElementRef, Input, OnChanges, OnDestroy, OnInit, SimpleChanges, ViewChild } from '@angular/core';
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
export class ConnectionMessagesComponent implements OnInit, OnChanges, OnDestroy{
  @ViewChild('messageForm') messageForm: NgForm;
  @Input() username:string;
  messages: Message[];
  user: User;
  messageContent: string;

  //userService is public right now, which is for message service, it needs to be separated
  constructor(public userService: UserService, private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges) {
    this.userService.stopHubConnection(); //stop the hub connection with previous active tab
    this.userService.createHubConnection(this.user, this.username); //start the hub connection with new one now
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
    this.userService.sendMessage(this.username, this.messageContent).then(() => {
      this.messageForm.reset();
    })
  }

  ngOnDestroy(): void {
    this.userService.stopHubConnection();
  }

}
