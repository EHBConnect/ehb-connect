import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FindProjects } from './find-projects';

describe('FindProjects', () => {
  let component: FindProjects;
  let fixture: ComponentFixture<FindProjects>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FindProjects]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FindProjects);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
