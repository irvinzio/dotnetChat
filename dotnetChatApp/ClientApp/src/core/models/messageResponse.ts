import { UserResponse } from "./userResponse";

export class MessageResponse {
    public messageId: string = '';
    public createdAt: Date = new Date();
    public text: string = '';
    public user: UserResponse = new UserResponse();
}