import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DriverOrderlistComponent } from './driver-orderlist.component';

describe('DriverOrderlistComponent', () => {
  let component: DriverOrderlistComponent;
  let fixture: ComponentFixture<DriverOrderlistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DriverOrderlistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DriverOrderlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
