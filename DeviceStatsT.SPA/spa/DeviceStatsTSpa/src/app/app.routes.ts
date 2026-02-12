import { Routes } from '@angular/router';
import { Home } from './home/home';
import { DevicesList } from './devices-list/devices-list';
import { DeviceStats } from './device-stats/device-stats';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'devices', component: DevicesList },
  { path: 'devices/:id', component: DeviceStats },
];
