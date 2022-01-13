import { User } from './../model/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../service/auth.service';
import { NavbarService } from '../service/navbar.service';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css'],
})
export class AccountComponent implements OnInit {
  user?: User | any | undefined;

  settingsForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    public nav: NavbarService,
    private router: Router,
    private authService: AuthService,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.nav.show();
    this.authService.isAuthenticated
      ? (this.user = this.authService.user)
      : (this.user = undefined);

    this.settingsForm.patchValue({ username: this.user.username });
  }

  updateUser() {
    this.http.patch<User>('api/User', this.user, this.httpOptions).subscribe(
      (data: User) => {
        this.user = data;
        this.authService.logout();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  update() {
    let user = {
      username: this.authService.user?.username,
      password: this.settingsForm.value.password,
    };

    if (!user.password) {
      return;
    }

    this.user = user;

    this.updateUser();
  }

  delete() {}
}
