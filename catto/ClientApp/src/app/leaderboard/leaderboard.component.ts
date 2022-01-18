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
  scores: Score[] = [];

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(public authService: AuthService, private http: HttpClient) {
    this.getUsersScore();
  }

  getUsersScore() {
    this.http.get<Score[]>('api/Score').subscribe(
      (result) => {
        this.scores = result;
      },
      (error) => console.error(error)
    );
  }

  ngOnInit(): void {}
}
