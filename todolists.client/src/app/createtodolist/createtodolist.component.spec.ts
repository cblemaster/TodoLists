import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatetodolistComponent } from './createtodolist.component';

describe('CreatetodolistComponent', () => {
  let component: CreatetodolistComponent;
  let fixture: ComponentFixture<CreatetodolistComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreatetodolistComponent]
    });
    fixture = TestBed.createComponent(CreatetodolistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
