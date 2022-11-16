import { Component, Inject, OnInit } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { SignalrService } from '../services/signalr.service';


@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  private baseUrl: string ;
  public signalRService: SignalrService;
  // listMessages: string[] = [];

  constructor(@Inject('BASE_URL') baseUrl: string, svc: SignalrService) {
    this.baseUrl = baseUrl;
    this.signalRService = svc;
  }

  ngOnInit(): void {
    console.log("ngInit");
    this.signalRService.startConnection();
    this.signalRService.addChatListener(); 
  }

  send(tb: HTMLInputElement) {
    console.log(tb.value);
    this.signalRService.sendMsg("Jeff", tb.value)
      .then(() => (tb.value = ""));
  }
}
