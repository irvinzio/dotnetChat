import { Component } from '@angular/core';
import { RegisterRequest } from 'src/core/models/registerRequest';
import { AuthorizationService } from 'src/services/authorization.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  msgDto: RegisterRequest = new RegisterRequest();
  comfirmaPassword : string = '';

  constructor(private authService: AuthorizationService) {}

  register(): void {
    if(this.msgDto) {
      if(this.msgDto.email.length == 0 || 
        this.msgDto.username.length == 0 ||
        this.msgDto.password.length == 0){
          window.alert("All fields are required.");
        return;
      } 
      else if(this.msgDto.password != this.comfirmaPassword){
          window.alert("passwords should be equal.");
        return;
      } 
      else {
        this.authService.register(this.msgDto)
        .subscribe(
          (response) => {  
            if(!response.success){
              console.log('error on registration',response);
              window.alert("something went wrong "+response.message);
            }else{
              console.log('registration succesful',response);
              window.alert("registration succesful, you can login with your email and password");
            }  
          },
          (error) => {                              
            console.error('error caught in component', error)
            window.alert("something went wrong see log for more detail");
          }
        );
      }
    }
  }
  
}
