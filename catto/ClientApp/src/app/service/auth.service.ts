import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public isAuthenticated = false;
  constructor(private router: Router) {}

  logout(): void {
    this.router.navigate(['/login']);
    this.isAuthenticated = false;
  }
}
