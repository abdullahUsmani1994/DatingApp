import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-Register',
  templateUrl: './Register.component.html',
  styleUrls: ['./Register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  @Output() cancelRegister = new EventEmitter();

  constructor(private authService:AuthService) { }

  ngOnInit() {
  }
  
  register() {
   this.authService.register(this.model).subscribe(() => {
     console.log("registration successful");
   }, error =>{
     console.log(error);
   });
  }
 
  cancel(){
    this.cancelRegister.emit(false);
    console.log("canceled");
  }
}
