import { MessageResponse } from "./messageResponse";

export class UserResponse {
    public userId: string = '';
    public email: string = '';
    public userName: string = '';
    public messages: MessageResponse[] = [];
}