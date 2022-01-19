import { ScoreService } from './../service/score.service';
import { AuthService } from './../service/auth.service';
import { Component, OnInit } from '@angular/core';
import { Score } from '../model/score';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.css'],
  
})
export class LeaderboardComponent implements OnInit {
  constructor(
    public authService: AuthService,
    public scoreService: ScoreService
  ) {}

  ngOnInit(): void {}
}
