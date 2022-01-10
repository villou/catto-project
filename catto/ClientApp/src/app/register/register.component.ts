import { NavbarService } from './../service/navbar.service';
import { Component, Inject, OnInit } from '@angular/core';
import { User } from '../model/user';
import { FormControl, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  user: User | any | undefined;

  registerForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    public nav: NavbarService,
    private http: HttpClient,
    private router: Router
  ) {}

  registerUser() {
    this.http
      .post<User>('api/User/register', this.user, this.httpOptions)
      .subscribe(
        (data: User) => {
          this.user = data;
        },
        (error) => {
          console.log(error);
        },
        () => this.router.navigate(['/login'])
      );
  }

  ngOnInit(): void {
    this.nav.hide();
  }

  register() {
    const userRegistred = {
      Username: this.registerForm.value.username,
      Password: this.registerForm.value.password,
    };

    this.user = userRegistred;
    this.registerUser();
  }
}
