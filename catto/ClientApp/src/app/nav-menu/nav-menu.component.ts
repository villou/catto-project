import { Component } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { NavbarService } from '../service/navbar.service';
import { faSignOutAlt } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent {
  constructor(public nav: NavbarService, public authService: AuthService) {}

  faSignOutAlt = faSignOutAlt;
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.authService.logout();
  }
}
