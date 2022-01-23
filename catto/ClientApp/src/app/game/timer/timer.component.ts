import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-timer',
  templateUrl: './timer.component.html',
  styleUrls: ['./timer.component.css'],
})
export class TimerComponent {
  @Input('time') time = 0;

  timeText() {
    if (this.time < 10) {
      return '00:0' + this.time;
    }
    return '00:' + this.time;
  }
}
