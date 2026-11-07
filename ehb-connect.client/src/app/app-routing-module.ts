import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Overview } from './pages/overview/overview';
import { FindProjects } from './pages/find-projects/find-projects';
import { SkillExchange } from './pages/skill-exchange/skill-exchange';
import { ProjectMatches } from './pages/project-matches/project-matches';
import { Messages } from './pages/messages/messages';
import { Profile } from './pages/profile/profile';

//Routes naar de vershillende views

const routes: Routes = [
  {
    path: '',
    component: Overview
  },
  {
    path: '',
    component: FindProjects
  },
  {
    path: '',
    component: SkillExchange
  },
  {
    path: '',
    component: ProjectMatches
  },
  {
    path: '',
    component: Messages
  },
  {
    path: '',
    component: Profile
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
