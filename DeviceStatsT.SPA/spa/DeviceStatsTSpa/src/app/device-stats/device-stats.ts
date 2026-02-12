import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AsyncPipe, NgFor, NgIf, DatePipe } from '@angular/common';
import { Observable, map, switchMap } from 'rxjs';
import { DevicesApi, DeviceStat } from '../devices-api';

@Component({
  selector: 'app-device-stats',
  imports: [AsyncPipe, NgFor, NgIf, DatePipe],
  templateUrl: './device-stats.html',
  styleUrl: './device-stats.less',
})
export class DeviceStats {
  deviceId$: Observable<string>;
  stats$: Observable<DeviceStat[]>;

  constructor(private route: ActivatedRoute, private api: DevicesApi) {
    this.deviceId$ = this.route.paramMap.pipe(
      map((pm) => pm.get('id') ?? '')
    );

    this.stats$ = this.deviceId$.pipe(
      switchMap((id) => this.api.getDeviceStats(id))
    );
  }
}
