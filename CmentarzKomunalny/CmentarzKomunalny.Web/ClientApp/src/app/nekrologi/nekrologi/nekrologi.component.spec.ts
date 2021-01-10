import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { NekrologiComponent } from './nekrologi.component';

describe('NekrologiComponent', () => {
  let component: NekrologiComponent;
  let fixture: ComponentFixture<NekrologiComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ NekrologiComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NekrologiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
