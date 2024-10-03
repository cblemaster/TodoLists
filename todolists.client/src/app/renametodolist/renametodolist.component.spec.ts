import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenametodolistComponent } from './renametodolist.component';

describe('RenametodolistComponent', () => {
  let component: RenametodolistComponent;
  let fixture: ComponentFixture<RenametodolistComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RenametodolistComponent]
    });
    fixture = TestBed.createComponent(RenametodolistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
