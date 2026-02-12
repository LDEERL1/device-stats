import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeviceStats } from './device-stats';

describe('DeviceStats', () => {
  let component: DeviceStats;
  let fixture: ComponentFixture<DeviceStats>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeviceStats]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeviceStats);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
