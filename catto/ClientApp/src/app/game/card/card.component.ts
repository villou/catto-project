import { GameService } from './../../service/game.service';
import { Component, Input, OnInit } from '@angular/core';
import { Score } from 'src/app/model/score';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css'],
})
export class CardComponent implements OnInit {
  @Input('title') title?: string;
  @Input('icon') icon?: string;
  @Input('description') description?: string;
  @Input('buttonText') buttonText?: string;
  @Input('score') score? = this.gameService.score;

  constructor(public gameService: GameService) {}

  startGame() {
    this.gameService.startGame();
  }

  ngOnInit(): void {}
}
