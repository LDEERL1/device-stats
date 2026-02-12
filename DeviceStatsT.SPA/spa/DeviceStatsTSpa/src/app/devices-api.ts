import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

export interface Device {
  id: string;
  name: string;
  version: string;
}

export interface DeviceStat {
  startTime: string;
  endTime: string;
}

@Injectable({ providedIn: 'root' })
export class DevicesApi {
  private readonly baseUrl = environment.apiBaseUrl; 

  constructor(private http: HttpClient) {}

  getDevices(): Observable<Device[]> {
    return this.http.get<Device[]>(`${this.baseUrl}/api/devices`);
  }

  getDeviceStats(deviceId: string): Observable<DeviceStat[]> {
    return this.http.get<DeviceStat[]>(`${this.baseUrl}/api/devices/${deviceId}/stats`);
  }
}
