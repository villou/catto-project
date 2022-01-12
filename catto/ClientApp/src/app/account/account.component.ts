import { User } from './../model/user';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../service/auth.service';
import { NavbarService } from '../service/navbar.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css'],
})
export class AccountComponent implements OnInit {
  user?: User | any | undefined;

  constructor(
    public nav: NavbarService,
    private router: Router,
    private authService: AuthService,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.nav.show();
  }

  getMe() {
    this.http.get<User>('api/User/me').subscribe(
      (result) => {
        this.user = result;
      },
      (error) => console.error(error)
    );
  }

  update() {}

  delete() {}
}
