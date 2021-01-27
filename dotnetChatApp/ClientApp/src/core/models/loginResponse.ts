import { UserResponse } from "./userResponse";

export class LogInResponse {
    public token: string = '';
    public expiration: string = '';
    public user: UserResponse;
}
