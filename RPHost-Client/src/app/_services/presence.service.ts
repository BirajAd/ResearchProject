import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr'
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ToastrService } from 'ngx-toastr';
import { User } from '../_models/user';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { AlertifyService } from './alertify.service';

@Injectable({
  providedIn: 'root'
})
export class PresenceService {
  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;
  private onlineUsersSource = new BehaviorSubject<string[]>([]);
  onlineUsers$ = this.onlineUsersSource.asObservable();

  constructor(private alertify: AlertifyService) { }

  createHubConnection(user: User)
  {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'presence', {
        accessTokenFactory: () => 
        { 
            return localStorage.getItem('token');
        }
      })
      .withAutomaticReconnect()
      .build()

    this.hubConnection
      .start()
      .catch(error => console.log(error));

    this.hubConnection.on('UserOnline', username =>
    {
      this.alertify.success(username + ' has connected');
    })

    this.hubConnection.on('UserOffline', username => {
      this.alertify.error(username + ' has disconnected.')
    })

    this.hubConnection.on('GetOnlineUsers', (usernames: string[]) => {
      this.onlineUsersSource.next(usernames);
    })
  }

  stopHubConnection() {
    this.hubConnection.stop().catch(error => console.log(error));
  }
}
