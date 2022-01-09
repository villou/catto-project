import { Component, OnInit } from '@angular/core';
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
  constructor(
    private router: Router,
    private authService: AuthService,
    public nav: NavbarService
  ) {
    if ((this.authService.isAuthenticated = true)) {
      this.router.navigate(['/home']);
    }
  }

  login() {}

  ngOnInit(): void {
    this.nav.hide();
  }
}
