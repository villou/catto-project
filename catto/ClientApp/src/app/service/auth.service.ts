import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../model/user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public user?: User;
  public isAuthenticated = false;
  userLoad = new EventEmitter<User>();

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    private router: Router,
    private httpLogout: HttpClient,
    http: HttpClient
  ) {
    this.isAuthenticated = Boolean(
      JSON.parse(localStorage.getItem('isAuthenticated') || 'false')
    );

    if (this.isAuthenticated) {
      http.get<User>('api/User/me').subscribe(
        (result) => {
          this.setCurrentUser(result);
        },
        (error) => console.error(error)
      );
    }
  }

  setCurrentUser(user?: User) {
    if (user) {
      this.userLoad.emit(user);
      this.user = user;
      this.isAuthenticated = true;
    } else {
      this.isAuthenticated = false;
    }
    localStorage.setItem(
      'isAuthenticated',
      JSON.stringify(this.isAuthenticated)
    );
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
          localStorage.removeItem('isAuthenticated');
          this.router.navigate(['/home']);
        }
      );
  }
}
