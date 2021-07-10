import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../_models/user';
import {PaginatedResult} from '../_models/pagination';
import { map, take } from 'rxjs/operators';
import { Message } from '../_models/message';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { error } from 'protractor';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;
  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;
  private messageThreadSource = new BehaviorSubject<Message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();

constructor(private http: HttpClient) { }

createHubConnection(user: User, otherUsername: string){
  this.hubConnection = new HubConnectionBuilder()
        .withUrl(this.hubUrl+'message?user='+otherUsername, {
          accessTokenFactory: () => { return localStorage.getItem("token"); }
        })
        .withAutomaticReconnect()
        .build()

  this.hubConnection.start().catch(error => console.log(error))
  
  this.hubConnection.on('ReceiveMessageThread', messages => {
    this.messageThreadSource.next(messages);
  })
  
  this.hubConnection.on('NewMessage', message => {
    this.messageThread$.pipe(take(1)).subscribe(messages => {
      this.messageThreadSource.next([...messages, message])
    })
  })
}

stopHubConnection() {
  if(this.hubConnection) {
    this.hubConnection.stop();
  }
}

getUsers(page?, itemsPerPage?, userParams?): Observable<PaginatedResult<User[]>> {
  const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();

  let params = new HttpParams();
  if (page != null && itemsPerPage != null) {
    params = params.append('pageNumber', page);
    params = params.append('pageSize', itemsPerPage);
  }
  
  if (userParams != null) {
    params = params.append('orderBy', userParams.orderBy);
  }

  return this.http.get<User[]>(this.baseUrl + 'users', {observe: 'response', params})
  .pipe(
    map(response => {
      paginatedResult.result = response.body;
      if (response.headers.get('Pagination') != null) {
        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'))
      }
      return paginatedResult;
    })
  );
}

getUser(id): Observable<User> {
  return this.http.get<User>(this.baseUrl + 'users/' + id);
}

updateUser(id: number, user: User) {
      return this.http.put(this.baseUrl + 'users/' + id, user);
}

setProfile(userId: number, id: number) {
    return this.http.post(this.baseUrl + '/users' + userId + '/photos/' + id + '/setProfile', {});
}

getMessages(page?, itemsPerPage?, messageContainer?){
  const paginatedResult: PaginatedResult<Message[]> = new PaginatedResult<Message[]>();

  let params = new HttpParams();

  params = params.append('MessageContainer', messageContainer);

  if (page != null && itemsPerPage != null) {
    params = params.append('pageNumber', page);
    params = params.append('pageSize', itemsPerPage);
  }

  return this.http.get<Message[]>(this.baseUrl + 'messages', {observe: 'response', params})
  .pipe(
    map(response => {
      paginatedResult.result = response.body;
      if(response.headers.get('Pagination') !== null) {
        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'))
      }

      return paginatedResult;
    })
  )
}

getMessageThread(recipientUsername: string){
    return this.http.get<Message[]>(this.baseUrl+'messages/'+recipientUsername);
}

async sendMessage(username: string, content: string){
  // return this.http.post<Message>(this.baseUrl+'messages/', {recipientUsername: username, content})
  return this.hubConnection.invoke('SendMessage', {recipientUsername: username, content})
    .catch(error => console.log(error));
}

sendFollow(id: number, recipientId: number){
  return this.http.post(this.baseUrl + 'users/' + id + '/follow/' + recipientId, {});
}

}
