import { ScoreService } from './../service/score.service';
import { User } from './../model/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../service/auth.service';
import { NavbarService } from '../service/navbar.service';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css'],
  providers: [ScoreService],
})
export class AccountComponent implements OnInit {
  settingsForm = new FormGroup({
    avatar: new FormControl(''),
    username: new FormControl(''),
    password: new FormControl(''),
  });

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    public nav: NavbarService,
    public authService: AuthService,
    private http: HttpClient
  ) {
    this.updateFormData(this.authService.user);
    authService.userLoad.subscribe((user) => this.updateFormData(user));
  }

  updateFormData(user?: User) {
    if (!user) return;
    this.settingsForm.setValue({
      avatar: user.avatar,
      username: user.username,
      password: '',
    });
  }

  ngOnInit(): void {
    this.nav.show();
  }

  update() {
    const user = {
      username: this.authService.user?.username,
      password: this.settingsForm.value.password,
      avatar: this.settingsForm.value.avatar,
    };

    if (user.password && user.avatar === null) {
      return;
    }

    this.http
      .put<User>(
        `api/User/${this.authService.user?.id}`,
        user,
        this.httpOptions
      )
      .subscribe(
        (data: User) => {
          this.authService.setCurrentUser(data);
        },
        (error) => {
          console.log(error);
        }
      );
  }
}
