import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Score } from '../model/score';

@Injectable({
  providedIn: 'platform',
})
export class ScoreService {
  scores?: Score[];

  get username() {
    return this.scores?.map((element) => {
      return element.username;
    });
  }

  get score() {
    return this.scores?.map((element) => {
      return element.score;
    });
  }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  constructor(private http: HttpClient) {
    this.http.get<Score[]>('api/Score').subscribe(
      (scores) => {
        this.scores = scores;
      },
      (error) => console.error(error)
    );
  }
}
