import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
})
export class CardComponent {
  @Input('title') title?: string;
  @Input('icon') icon?: string;
  @Input('description') description?: string;
  @Input('buttonText') buttonText?: string;
  @Input('score') score?: number;
  @Output('start') start = new EventEmitter();

  constructor() {}

  startGame() {
    this.start.emit();
  }
}
