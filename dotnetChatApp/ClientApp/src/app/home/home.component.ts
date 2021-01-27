import { Component } from '@angular/core';
import { MessageRequest } from 'src/core/models/messageRequest';
import { MessageResponse } from 'src/core/models/messageResponse';
import { ChatService } from 'src/services/chat.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  title = 'web-chat-app';
  msgDto: MessageRequest = new MessageRequest();
  msgInboxArray: MessageResponse[] = [];

  constructor(private chatService: ChatService) {
    this.msgDto.email = localStorage.getItem("email");
    this.msgDto.userId = localStorage.getItem("userId");
  }

  ngOnInit(): void {
    this.chatService.retrieveMappedObject().subscribe( (receivedObj: MessageRequest) => { this.addToInbox(receivedObj);}); 
    this.getMessages()                                               
  }

  send(): void {
    if(this.msgDto) {
      if(this.msgDto.text.length == 0){
        window.alert("text is required.");
        return;
      } else {
        this.chatService.broadcastMessage(this.msgDto)
        .subscribe(
          (response) => {  
            console.log("message send ", response);  
          },
          (error) => {                              
            console.error('error caught in component', error)
            window.alert("something went wrong see log for more detail ");
          }
        );
      }
    }
  }
  getMessages(): void {
      this.chatService.getMessages()
      .subscribe(
        (response) => {  
          console.log("all message retrieve", response);  
          this.msgInboxArray = response;
        },
        (error) => {                              
          console.error('error caught in component', error)
          window.alert("something went wrong see log for more detail ");
        }
      );
  }
  addToInbox(obj: MessageRequest) {
    let newObj = new MessageResponse();
    newObj.user.email = obj.email;
    newObj.text = obj.text;
    newObj.user.userId = obj.userId;
    this.msgInboxArray.push(newObj);

  }
}
