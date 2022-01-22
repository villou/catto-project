import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../model/user';
import { AuthService } from '../service/auth.service';
import { NavbarService } from '../service/navbar.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    private router: Router,
    private authService: AuthService,
    private http: HttpClient,
    public nav: NavbarService
  ) {}

  ngOnInit(): void {
    this.nav.hide();
  }

  login() {
    const userLogged = {
      username: this.loginForm.value.username,
      password: this.loginForm.value.password,
    };

    if (!userLogged.username || !userLogged.password) {
      return;
    }

    this.http
      .post<User>('api/User/login', userLogged, this.httpOptions)
      .subscribe(
        (data: User) => {
          this.authService.setCurrentUser(data);
          this.router.navigate(['/home']);
        },
        (error) => {
          console.log(error);
        }
      );
  }
}
