import { NavbarService } from './../service/navbar.service';
import { Component, OnInit } from '@angular/core';
import { User } from '../model/user';
import {
  FormControl,
  FormGroup,
  Validators,
  FormsModule,
} from '@angular/forms';
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
    username: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(10),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
      Validators.maxLength(10),
    ]),
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
