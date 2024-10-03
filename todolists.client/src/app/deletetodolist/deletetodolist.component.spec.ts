import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeletetodolistComponent } from './deletetodolist.component';

describe('DeletetodolistComponent', () => {
  let component: DeletetodolistComponent;
  let fixture: ComponentFixture<DeletetodolistComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeletetodolistComponent]
    });
    fixture = TestBed.createComponent(DeletetodolistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
