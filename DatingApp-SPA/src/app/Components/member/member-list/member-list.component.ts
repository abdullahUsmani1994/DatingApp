import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/User';
import { AlertifyService } from 'src/app/_services/Alertify.service';
import { UserService } from 'src/app/_services/user.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html'
})
export class MemberListComponent implements OnInit {

  users: User[];
  constructor(private userService: UserService, private alertify: AlertifyService, private route:ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'];
    })
  }

  // loadUser(){
  //   this.userService.getUsers().subscribe((users: User[]) => {
  //     this.users = users;
  //     console.log(users);
      
  //   },error =>{
  //     this.alertify.error(error);
  //   })
  // }
}

