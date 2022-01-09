import { Injectable } from '@angular/core';

@Injectable()
export class NavbarService {
  visible: boolean;

  constructor() {
    this.visible = true;
  }

  hide() {
    this.visible = false;
  }
}
