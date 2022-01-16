import { GameService } from './../service/game.service';
import { NavbarService } from './../service/navbar.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { TimerService } from '../service/timer.service';
import { faPaw } from '@fortawesome/free-solid-svg-icons';
import { Image } from '../model/image';
import { CardComponent } from './card/card.component';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
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
    private authService: AuthService,
    public timerService: TimerService,
    public gameService: GameService
  ) {
    this.gameService.startGame();
  }

  ngOnInit(): void {
    this.nav.show();
    this.authService?.user;
  }

  cat() {
    this.timerService.startTimer(15);
  }

  noCat() {
    console.log('no cat');
  }
}
