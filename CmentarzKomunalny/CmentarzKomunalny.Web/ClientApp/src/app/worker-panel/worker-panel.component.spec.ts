import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { WorkerPanelComponent } from './worker-panel.component';

describe('WorkerPanelComponent', () => {
  let component: WorkerPanelComponent;
  let fixture: ComponentFixture<WorkerPanelComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkerPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkerPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
