import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchesHomeComponent } from './matches-home.component';

describe('MatchesHomeComponent', () => {
  let component: MatchesHomeComponent;
  let fixture: ComponentFixture<MatchesHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MatchesHomeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MatchesHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
