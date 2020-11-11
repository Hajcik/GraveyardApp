import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InformacjeComponent } from './informacje.component';

describe('InformacjeComponent', () => {
  let component: InformacjeComponent;
  let fixture: ComponentFixture<InformacjeComponent>;

  beforeEach(async(() => {
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
