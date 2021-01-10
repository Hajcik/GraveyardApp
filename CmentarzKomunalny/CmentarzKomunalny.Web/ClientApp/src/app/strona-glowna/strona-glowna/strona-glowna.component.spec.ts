import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StronaGlownaComponent } from './strona-glowna.component';

describe('StronaGlownaComponent', () => {
  let component: StronaGlownaComponent;
  let fixture: ComponentFixture<StronaGlownaComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ StronaGlownaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StronaGlownaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
