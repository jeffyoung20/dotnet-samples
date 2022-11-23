import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private baseUrl: string;
  private readonly _http: HttpClient;
  private hubConnection!: signalR.HubConnection; 
  private listMessages: string[] = [];
  public listMessages$: Observable<string[]> = of(this.listMessages)
      .pipe(
        tap(msg => console.log(msg)) //for debug
      )
  
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    this._http = http;
    this.baseUrl = baseUrl;
  }

  public startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl(this.baseUrl +"hub")
                            .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public addChatListener() {
    this.hubConnection.on('messageReceived', (_username: string, message: string)  => {
      console.log("Message Received: " + message);
      this.listMessages.push(message);
    });
  }

  public sendMsg(userName: string, msg: string) {
    return this.hubConnection.send("NewMessage", userName, msg);
  }
}
