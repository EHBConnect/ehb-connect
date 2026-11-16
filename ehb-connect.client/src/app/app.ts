import { Component, signal } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Ehb connect');

  constructor(public router: Router) { }

  isAuthPage(): boolean {
    const path = this.router.url.split('?')[0];

    return (
      path === '/' ||
      path === '/welcome' ||
      path === '/login' ||
      path === '/register'
    );
  }
}
