import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-avatar',
  templateUrl: './avatar.component.html',
})
export class AvatarComponent {
  @Input('avatarUrl') avatarUrl?: string;

  constructor() {
    this.avatarUrl = this.avatarUrl
      ? this.avatarUrl
      : 'https://64.media.tumblr.com/3a1730fccc0f8144e4823b333383855d/tumblr_ozu6r9kdg31rxye79o1_1280.jpg';
  }
}
