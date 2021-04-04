import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Message } from '../_models/message';

@Injectable()

export class MessagesResolver implements Resolve<Message[]> {
    pageNumber = 1;
    pageSize = 20;
    messageContainer = 'All';

    constructor(private userService: UserService, private router: Router,
                private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Message[]> {
        return this.userService.getMessages(this.pageNumber, this.pageSize, this.messageContainer )
        .pipe(
            catchError(error => {
                this.alertify.error('Problem returieving messages');
                this.router.navigate(['/home']);
                return of(null);
            })
        )
    }


}