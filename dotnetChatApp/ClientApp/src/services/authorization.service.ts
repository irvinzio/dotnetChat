import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { LogInRequest } from 'src/core/models/loginRequest';
import { LogInResponse } from 'src/core/models/loginResponse';
import { RegisterRequest } from 'src/core/models/registerRequest';
import { RegisterResponse } from 'src/core/models/registerResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  private readonly REGISTER_URL = "https://localhost:5001/api/security/register";
  private readonly LOGIN_URL = "https://localhost:5001/api/security/login";

  constructor(private http: HttpClient) { }
  
  public register(msgDto: RegisterRequest) : Observable<RegisterResponse>{
    return this.http.post<RegisterResponse>(this.REGISTER_URL, msgDto)
  }
  public login(msgDto: LogInRequest): Observable<LogInResponse> {
    return this.http.post<LogInResponse>(this.LOGIN_URL, msgDto)
  }
}
