import { ScoreService } from './../../service/score.service';
import { GameService } from '../../service/game.service';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements OnInit {
  constructor(public scoreService: ScoreService) {}

  ngOnInit(): void {}
}
