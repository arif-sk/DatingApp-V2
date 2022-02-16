import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'DatingApp-Angular';
  users: any;
  constructor(
    private http: HttpClient,
    private accountService: AccountService
  ){

  }
  ngOnInit() {
      this.getUsers();
      this.setCurentuser();
  }

  getUsers() {
    this.http.get('https://localhost:5000/api/users').subscribe(response => {
      this.users = response;
    }, err => {
      console.error(err);
    });
  }

  setCurentuser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

}
