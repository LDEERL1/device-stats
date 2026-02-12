import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DevicesList } from './devices-list';

describe('DevicesList', () => {
  let component: DevicesList;
  let fixture: ComponentFixture<DevicesList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DevicesList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DevicesList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
