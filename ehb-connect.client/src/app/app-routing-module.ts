import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Overview } from './pages/overview/overview';
import { SkillExchange } from './pages/skill-exchange/skill-exchange';
import { ProjectMatches } from './pages/project-matches/project-matches';
import { Messages } from './pages/messages/messages';
import { Profile } from './pages/profile/profile';
import { FindProjects } from './pages/find-projects/find-projects';

//Routes naar de vershillende views

const routes: Routes = [
  {
    path: '',
    component: Overview
  },
  {
    path: 'find-projects',
    component: FindProjects
  },
  {
    path: 'skill-exchange',
    component: SkillExchange
  },
  {
    path: 'project-matches',
    component: ProjectMatches
  },
  {
    path: 'messages',
    component: Messages
  },
  {
    path: 'profile',
    component: Profile
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
