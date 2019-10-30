import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/Alertify.service';


@Component({
  selector: 'app-Register',
  templateUrl: './Register.component.html',
  styleUrls: ['./Register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  @Output() cancelRegister = new EventEmitter();

  constructor(private authService:AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }
  
  register() {
   this.authService.register(this.model).subscribe(() => {
    this.alertify.success("registration successful");
   }, error => {
    this.alertify.error(error);
    debugger
   });
  }
 
  cancel(){
    this.cancelRegister.emit(false);
    this.alertify.message("canceled");
  }
}
