import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { WyszukiwarkaGrobowComponent } from './wyszukiwarka-grobow.component';

describe('WyszukiwarkaGrobowComponent', () => {
  let component: WyszukiwarkaGrobowComponent;
  let fixture: ComponentFixture<WyszukiwarkaGrobowComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ WyszukiwarkaGrobowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WyszukiwarkaGrobowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
