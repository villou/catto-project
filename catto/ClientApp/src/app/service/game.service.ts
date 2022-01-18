import { imageBase } from './../model/image';
import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import { TimerService } from './timer.service';
import { Image } from './../model/image';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Score } from '../model/score';

type State = 'waiting' | 'playing' | 'finished';
@Injectable({
  providedIn: 'root',
})
export class GameService {
  state: State = 'waiting';
  imagesStack: Image[] = [];
  imagesDone: Image[] = [];
  currentImage?: Image;

  get score() {
    return this.imagesDone?.length;
  }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    private timerService: TimerService,
    private authService: AuthService,
    private http: HttpClient
  ) {
    timerService.onFinish.subscribe(() => {
      this.endGame();
    });
  }

  setState(newState: State) {
    this.state = newState;
  }

  public startGame() {
    this.imagesDone = [];
    this.setState('playing');
    this.shuffle();
    this.popStack();
    this.timerService.startTimer(15);
  }

  public endGame() {
    this.timerService.stopTimer();
    this.setState('finished');
    this.saveScore();
  }

  public setDone() {
    if (this.currentImage) {
      this.imagesDone.push(this.currentImage);
      this.popStack();
    }
  }

  public popStack() {
    this.currentImage = this.imagesStack.pop();
  }

  public shuffle() {
    this.imagesStack = shuffleArray(imageBase);
  }

  public checkCurrentImage(isCat: boolean) {
    if (this.currentImage?.isCat !== isCat) {
      this.endGame();
      return;
    }
    this.setDone();
    this.timerService.startTimer(15);
  }

  saveScore() {
    console.log(this.score);
    this.http
      .post<Score>('api/Score/save', { score: this.score }, this.httpOptions)
      .subscribe(
        (data: Score) => {
          console.log('score saved');
        },
        (error) => {
          console.log(error);
        }
      );
  }
}

const shuffleArray = <T>(array: T[]): T[] =>
  array
    .map((value) => ({ value, sort: Math.random() }))
    .sort((a, b) => a.sort - b.sort)
    .map(({ value }) => value);
