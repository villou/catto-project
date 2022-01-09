import { NavbarService } from './../service/navbar.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../model/user';
import { AuthService } from '../service/auth.service';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  constructor(
    private router: Router,
    private authService: AuthService,
    public nav: NavbarService
  ) {}

  register() {}
  ngOnInit(): void {
    this.nav.hide();
  }
}
