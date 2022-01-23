import { AuthService } from './../../service/auth.service';
import { ScoreService } from './../../service/score.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
})
export class UserComponent {
  constructor(
    public scoreService: ScoreService,
    public authService: AuthService
  ) {}
}
