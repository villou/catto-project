import { NavbarService } from './../service/navbar.service';
import { Component, OnInit } from '@angular/core';
import { User } from '../model/user';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})
export class RegisterComponent implements OnInit {
  user: User | any | undefined;

  registerForm = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
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
      username: this.registerForm.value.username,
      password: this.registerForm.value.password,
    };

    if (!userRegistred.username) {
      return;
    }

    if (!userRegistred.password) {
      return;
    }

    this.user = userRegistred;
    this.registerUser();
  }
}
