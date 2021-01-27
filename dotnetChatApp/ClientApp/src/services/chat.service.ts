import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { MessageRequest } from 'src/core/models/messageRequest';
import * as signalR from '@microsoft/signalr';
import { MessageResponse } from 'src/core/models/messageResponse';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

   private  connection: any = new signalR.HubConnectionBuilder().withUrl("https://localhost:5001/chatsocket")   // mapping to the chathub as in startup.cs
                                         .configureLogging(signalR.LogLevel.Information)
                                         .build();
   readonly POST_URL = "https://localhost:5001/api/chat/send";
   readonly GET_MESSAGES_URL = "https://localhost:5001/api/chat/retrieve";

  private receivedMessageObject: MessageRequest = new MessageRequest();
  private sharedObj = new Subject<MessageRequest>();
  
  constructor(private http: HttpClient) { 
    this.connection.onclose(async () => {
      await this.start();
    });
   this.connection.on("ReceiveMessage", (email, text, userId) => { this.mapReceivedMessage(email, text, userId); });
   this.start();    
    
  }

  public async start() {
    try {
      await this.connection.start();
      console.log("connected");
    } catch (err) {
      console.log(err);
      setTimeout(() => this.start(), 5000);
    } 
  }

  private mapReceivedMessage(email: string, text: string,  userId: string): void {
    this.receivedMessageObject.email = email;
    this.receivedMessageObject.text = text;
    this.receivedMessageObject.userId = userId;
    this.sharedObj.next(this.receivedMessageObject);
 }

  public broadcastMessage(msgDto: any) : Observable<any>{
    console.log("sending with header", localStorage.getItem("token"));
    //this.connection.invoke("ReceiveMessage", msgDto.user, msgDto.text).catch(err => console.error(err));
    return this.http.post(this.POST_URL, msgDto, { headers: this.createAuthorizationHeader() });
  }
  public getMessages() : Observable<MessageResponse[]>{
    return this.http.get<MessageResponse[]>(this.GET_MESSAGES_URL, { headers: this.createAuthorizationHeader() });
  }
  public retrieveMappedObject(): Observable<MessageRequest> {
    return this.sharedObj.asObservable();
  }
  createAuthorizationHeader(): HttpHeaders {
    const headerDict = {
      Authorization: 'Bearer '+localStorage.getItem("token"),
    }
    return new HttpHeaders(headerDict);
  }

}
