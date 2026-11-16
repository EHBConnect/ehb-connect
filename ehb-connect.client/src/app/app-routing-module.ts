import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Overview } from './pages/overview/overview';
import { SkillExchange } from './pages/skill-exchange/skill-exchange';
import { ProjectMatches } from './pages/project-matches/project-matches';
import { Messages } from './pages/messages/messages';
import { Profile } from './pages/profile/profile';
import { FindProjects } from './pages/find-projects/find-projects';
import { Welcome } from './pages/welcome/welcome';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';

//Routes naar de verschillende views

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'welcome'
  },
  {
    path: 'welcome',
    component: Welcome
  },
  {
    path: 'login',
    component: Login
  },
  {
    path: 'register',
    component: Register
  },
  {
    path: 'overview',
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
  {
    path: '**',
    redirectTo: 'welcome'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

