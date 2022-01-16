import { Component, Input, OnInit } from '@angular/core';

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

  constructor() {}

  ngOnInit(): void {}
}
