import { Component } from '@angular/core';
import { AuthorizationService } from 'src/services/authorization.service';
import { LogInRequest } from 'src/core/models/loginRequest';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  msgDto: LogInRequest = new LogInRequest();
  comfirmaPassword : string = '';

  constructor(private authService: AuthorizationService,
    private router: Router) {}

  login(): void {
    if(this.msgDto) {
      if(this.msgDto.email.length == 0 || 
        this.msgDto.password.length == 0){
          window.alert("All fields are required.");
        return;
      } 
      else {
        this.authService.login(this.msgDto)
        .subscribe(
          (response) => {  
            localStorage.setItem('token', response.token); 
            localStorage.setItem('tokenExpiration', response.expiration); 
            localStorage.setItem("email",response.user.email);
            localStorage.setItem("userId",response.user.userId);
            localStorage.setItem("userName",response.user.userName);
            this.router.navigate(['/home']);   
          },
          (error) => {                              
            console.error('error caught in component', error)
            window.alert("something went wrong see log for more detail ");
          }
        );
      }
    }
  }
}
