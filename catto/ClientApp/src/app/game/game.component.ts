import { NavbarService } from './../service/navbar.service';
import { Component, OnInit } from '@angular/core';
import { User } from '../model/user';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
})
export class GameComponent implements OnInit {
  user?: User | any | undefined;
  constructor(public nav: NavbarService, private authService: AuthService) {}

  ngOnInit(): void {
    this.nav.show();
    this.authService.isAuthenticated
      ? (this.user = this.authService.user)
      : (this.user = undefined);
  }
}
