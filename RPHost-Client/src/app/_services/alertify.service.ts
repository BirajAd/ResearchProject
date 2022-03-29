import { Injectable } from '@angular/core';
import { confirm, success, error, warning, message } from 'alertifyjs';


@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

constructor() { }

  confirm(message: string, okCallback: () => any) {
    confirm(message, (e: any) => {
      if(e) {
        okCallback();
      } else {}
    });
  }

  success(message: string) {
    success(message);
  }

  error(message: string) {
    error(message);
  }

  warning(message: string) {
    warning(message);
  }

  message(msg: string) {
    message(msg);
  }

}
