import { ScoreService } from './../service/score.service';
import { GameService } from './../service/game.service';
import { NavbarService } from './../service/navbar.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { TimerService } from '../service/timer.service';
import { faPaw } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
  providers: [ScoreService, GameService, TimerService],
})
export class GameComponent implements OnInit {
  faPaw = faPaw;

  get time() {
    return this.timerService.remainingTime;
  }

  get score() {
    return this.gameService.score;
  }

  constructor(
    public nav: NavbarService,
    public authService: AuthService,
    public timerService: TimerService,
    public gameService: GameService
  ) {}

  ngOnInit(): void {
    this.nav.show();
  }
}
