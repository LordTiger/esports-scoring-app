import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserMatchHistoryComponent } from './user-match-history.component';

describe('UserMatchHistoryComponent', () => {
  let component: UserMatchHistoryComponent;
  let fixture: ComponentFixture<UserMatchHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserMatchHistoryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserMatchHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
