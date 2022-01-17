import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TimerService {
  remainingTime = 0;
  isStart = false;
  onFinish = new EventEmitter<number>();
  private timeout?: number;

  constructor() {}

  public startTimer(duration: number) {
    clearInterval(this.timeout);
    this.remainingTime = duration;
    this.isStart = true;
    this.runTimer();
  }

  public stopTimer() {
    this.isStart = false;
    clearInterval(this.timeout);
  }

  runTimer() {
    const intervalId = window.setInterval(() => {
      if (this.isStart) {
        this.remainingTime--;
      } else {
        this.stopTimer();
      }
      if (this.remainingTime === 0) {
        this.stopTimer();
        this.onFinish.emit();
      }
    }, 1000);
    this.timeout = intervalId;
  }
}
