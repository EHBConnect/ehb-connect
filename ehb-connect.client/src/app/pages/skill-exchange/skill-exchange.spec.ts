import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SkillExchange } from './skill-exchange';

describe('SkillExchange', () => {
  let component: SkillExchange;
  let fixture: ComponentFixture<SkillExchange>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SkillExchange]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SkillExchange);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
