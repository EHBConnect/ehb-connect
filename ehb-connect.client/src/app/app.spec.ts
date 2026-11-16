import { TestBed } from '@angular/core/testing';
import { RouterModule } from '@angular/router';

import { App } from './app';
import { Sidebar } from './components/sidebar/sidebar';

// Replaces the template test for the removed weather demo
// (ngOnInit/forecasts no longer exist, so the old test didn't compile).
describe('App', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterModule.forRoot([])],
      declarations: [App, Sidebar]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(App);
    expect(fixture.componentInstance).toBeTruthy();
  });
});
