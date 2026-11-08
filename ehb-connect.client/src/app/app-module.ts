import { HttpClientModule } from '@angular/common/http';
import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { FindProjects } from './pages/find-projects/find-projects';
import { Overview } from './pages/overview/overview';
import { SkillExchange } from './pages/skill-exchange/skill-exchange';
import { ProjectMatches } from './pages/project-matches/project-matches';
import { Messages } from './pages/messages/messages';
import { Profile } from './pages/profile/profile';

@NgModule({
  declarations: [
    App,
    FindProjects,
    Overview,
    SkillExchange,
    ProjectMatches,
    Messages,
    Profile
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners()
  ],
  bootstrap: [App]
})
export class AppModule { }
