import { Component } from '@angular/core';
import { MessageRequest } from 'src/core/models/messageRequest';
import { ChatService } from 'src/services/chat.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  title = 'web-chat-app';
  constructor(private chatService: ChatService) {}

  ngOnInit(): void {
    this.chatService.retrieveMappedObject().subscribe( (receivedObj: MessageRequest) => { this.addToInbox(receivedObj);});
                                                     
  }

  msgDto: MessageRequest = new MessageRequest();
  msgInboxArray: MessageRequest[] = [];

  send(): void {
    if(this.msgDto) {
      if(this.msgDto.user.length == 0 || this.msgDto.user.length == 0){
        window.alert("Both fields are required.");
        return;
      } else {
        this.chatService.broadcastMessage(this.msgDto);
      }
    }
  }

  addToInbox(obj: MessageRequest) {
    let newObj = new MessageRequest();
    newObj.user = obj.user;
    newObj.msgText = obj.msgText;
    this.msgInboxArray.push(newObj);

  }
}
