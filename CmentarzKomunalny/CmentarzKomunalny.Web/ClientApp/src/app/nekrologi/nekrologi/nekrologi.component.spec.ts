import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NekrologiComponent } from './nekrologi.component';

describe('NekrologiComponent', () => {
  let component: NekrologiComponent;
  let fixture: ComponentFixture<NekrologiComponent>;

  beforeEach(async(() => {
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
