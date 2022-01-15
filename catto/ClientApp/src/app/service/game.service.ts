import { imageBase } from './../model/image';
import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import { TimerService } from './timer.service';
import { Image } from './../model/image';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  imagesStack: Image[] = [];
  imagesDone: Image[] = [];
  currentImage?: Image;
  get score() {
    return this.imagesDone?.length;
  }

  constructor(
    private timerService: TimerService,
    private authService: AuthService
  ) {
    timerService.onFinish.subscribe(() => {
      this.endGame();
    });
  }

  public startGame() {
    this.shuffle();
    this.popStack();
    this.timerService.startTimer(15);
  }

  public endGame() {
    this.timerService.stopTimer();
  }

  public setDone() {
    if (!this.currentImage) {
      return;
    }
    this.imagesDone.push(this.currentImage);
    this.popStack();
  }

  public popStack() {
    this.currentImage = this.imagesStack.pop();
  }

  public shuffle() {
    this.imagesStack = shuffleArray(imageBase);
  }
}

const shuffleArray = <T>(array: T[]): T[] =>
  array
    .map((value) => ({ value, sort: Math.random() }))
    .sort((a, b) => a.sort - b.sort)
    .map(({ value }) => value);
