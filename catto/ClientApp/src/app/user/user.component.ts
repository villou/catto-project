import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements OnInit {
  //@Input('avatar') avatar?: string;
  @Input('username') username?: string;
  @Input('score') score?: number;

  constructor() {}

  ngOnInit(): void {}
}
