import { GameService } from './../service/game.service';
import { NavbarService } from './../service/navbar.service';
import { Component, OnInit } from '@angular/core';
import { User } from '../model/user';
import { AuthService } from '../service/auth.service';
import { TimerService } from '../service/timer.service';
import { Score } from '../model/score';
import { faPaw } from '@fortawesome/free-solid-svg-icons';
import { Image } from '../model/image';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
})
export class GameComponent implements OnInit {
  user?: User | any | undefined;
  score?: Score;
  faPaw = faPaw;

  get time() {
    return this.timerService.remainingTime;
  }

  constructor(
    public nav: NavbarService,
    private authService: AuthService,
    public timerService: TimerService,
    public gameService: GameService
  ) {
    this.gameService.startGame();
  }

  ngOnInit(): void {
    this.nav.show();
    this.authService.isAuthenticated
      ? (this.user = this.authService.user)
      : (this.user = undefined);
  }

  cat() {
    this.timerService.startTimer(15);
  }

  noCat() {
    console.log('no cat');
  }
}
