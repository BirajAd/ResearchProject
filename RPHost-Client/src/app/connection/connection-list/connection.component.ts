import { Component, Injectable, OnInit } from '@angular/core';
import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';


@Component({
  selector: 'app-connection',
  templateUrl: './connection.component.html',
  styleUrls: ['./connection.component.css']
})
@Injectable()
export class ConnectionComponent implements OnInit {
  users: User[];
  user: User = JSON.parse(localStorage.getItem('user'));
  pagination: Pagination;
  userParams: any = {} ;

  constructor(private userService: UserService, private alertify: AlertifyService, private route: ActivatedRoute) { }
  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'].result;
      this.pagination = data['users'].pagination;
      // console.log("from pagination: "+data['users'].pagination);
    });

    this.userParams.orderBy = 'firstName';
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    console.log(this.pagination.currentPage);
  }

loadUsers() {
  this.userService.getUsers(this.pagination.currentPage, this.pagination.itemsPerPage)
  .subscribe(
    (res: PaginatedResult<User[]>) => {
     this.users = res.result;
     this.pagination = res.pagination;
     }, error => {
       this.alertify.error(error);
   });
  }
}
