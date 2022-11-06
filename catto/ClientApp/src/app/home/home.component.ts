import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../service/auth.service';
import { NavbarService } from '../service/navbar.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  constructor(
    public nav: NavbarService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.nav.show();
  }

  play() {
  this.router.navigate(['/game']);
  }
}
