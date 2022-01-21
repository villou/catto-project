import { ScoreService } from './../service/score.service';
import { AuthService } from './../service/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.css'],
  providers: [ScoreService],
})
export class LeaderboardComponent implements OnInit {
  constructor(
    public authService: AuthService,
    public scoreService: ScoreService
  ) {}

  ngOnInit(): void {}
}
