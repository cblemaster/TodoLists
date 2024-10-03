import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatetodoComponent } from './updatetodo.component';

describe('UpdatetodoComponent', () => {
  let component: UpdatetodoComponent;
  let fixture: ComponentFixture<UpdatetodoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UpdatetodoComponent]
    });
    fixture = TestBed.createComponent(UpdatetodoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
