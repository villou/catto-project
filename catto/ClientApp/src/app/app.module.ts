import { AuthService } from './service/auth.service';
import { NavbarService } from './service/navbar.service';
import { LoginComponent } from './login/login.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { GameComponent } from './game/game.component';
import { LeaderboardComponent } from './leaderboard/leaderboard.component';
import { AccountComponent } from './account/account.component';
import { UserComponent } from './leaderboard/user/user.component';
import { RegisterComponent } from './register/register.component';
import { AvatarComponent } from './avatar/avatar.component';
import { AuthGuard } from './guard/auth.guard';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CardComponent } from './game/card/card.component';
import { TimerComponent } from './game/timer/timer.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full',
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  { path: 'game', component: GameComponent },
  {
    path: 'leaderboard',
    component: LeaderboardComponent,
  },
  { path: 'account', component: AccountComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    GameComponent,
    LeaderboardComponent,
    AccountComponent,
    UserComponent,
    LoginComponent,
    RegisterComponent,
    AvatarComponent,
    CardComponent,
    TimerComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forRoot(routes),
    FontAwesomeModule,
  ],
  providers: [NavbarService, AuthService],
  bootstrap: [AppComponent],
})
export class AppModule {}
