import { GameService } from './../../service/game.service';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css'],
})
export class CardComponent {
  @Input('title') title?: string;
  @Input('icon') icon?: string;
  @Input('description') description?: string;
  @Input('buttonText') buttonText?: string;
  @Input('score') score? = this.gameService.score;
  @Output() start = new EventEmitter();

  constructor(public gameService: GameService) {}

  startGame() {
    this.start.emit(this.gameService.startGame());
  }
}
