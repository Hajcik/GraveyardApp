import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { RoczniceComponent } from './rocznice.component';

describe('RoczniceComponent', () => {
  let component: RoczniceComponent;
  let fixture: ComponentFixture<RoczniceComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ RoczniceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoczniceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
