import { Component, Input, OnInit } from '@angular/core';
import { Message } from '../_models/message'
import { PaginationComponent } from 'ngx-bootstrap/pagination';
import { PaginatedResult, Pagination } from '../_models/pagination';
import { UserService } from '../_services/user.service';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { User } from '../_models/user';
import { Observable } from 'rxjs';

@Component({
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messages: Message[];
  username: string;
  pagination: Pagination; //refer to message resolver to customize pagesize and pageNumber
  messageContainer = 'Inbox';

  constructor(private userService: UserService, private route: ActivatedRoute, private alertify: AlertifyService,
        private authService: AuthService) {}

  ngOnInit()
  {
      this.route.data.subscribe(data => {
        this.messages = data['messages'].result;
        this.pagination = data['messages'].pagination;
        this.username = localStorage.username;
      });
  }

  loadMessages() {
    this.userService.getMessages(this.pagination.currentPage, this.pagination.itemsPerPage, this.messageContainer)
    .subscribe((res: PaginatedResult<Message[]>) => {
      this.messages = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadMessages();
  }

}
