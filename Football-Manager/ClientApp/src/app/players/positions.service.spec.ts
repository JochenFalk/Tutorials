import { TestBed } from '@angular/core/testing';

import { PositionsService } from './positions.service';

describe('PositionsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PositionsService = TestBed.get(PositionsService);
    expect(service).toBeTruthy();
  });
});
