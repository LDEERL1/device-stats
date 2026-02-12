import { TestBed } from '@angular/core/testing';

import { DevicesApi } from './devices-api';

describe('DevicesApi', () => {
  let service: DevicesApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DevicesApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
