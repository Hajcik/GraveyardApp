import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { InformacjeComponent } from './informacje.component';

describe('InformacjeComponent', () => {
  let component: InformacjeComponent;
  let fixture: ComponentFixture<InformacjeComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ InformacjeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InformacjeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
