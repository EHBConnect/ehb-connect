import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectMatches } from './project-matches';

describe('ProjectMatches', () => {
  let component: ProjectMatches;
  let fixture: ComponentFixture<ProjectMatches>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProjectMatches]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProjectMatches);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
