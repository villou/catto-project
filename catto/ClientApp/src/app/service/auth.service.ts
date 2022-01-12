import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../model/user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public user?: User;
  public isAuthenticated = false;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    private router: Router,
    private httpLogout: HttpClient,
    private http: HttpClient
  ) {
    //fetch me & set le user
  }

  getCurrentUser(user: User) {
    this.http.get<User>('api/User/me').subscribe(
      (result) => {
        this.user = result;
      },
      (error) => console.error(error)
    );

    if (user) {
      this.user = user;
      this.isAuthenticated = true;
    }
    return false;
  }

  logout(): void {
    this.httpLogout
      .post<User>('api/User/logout', this.user, this.httpOptions)
      .subscribe(
        (data: User) => {
          this.user = data;
        },
        (error) => {
          console.log(error);
        },
        () => {
          this.isAuthenticated = false;
          this.router.navigate(['/login']);
        }
      );
  }
}
