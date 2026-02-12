import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs'
export interface Device{
    id: string;
    name: string;
    version: string;
  }
  export interface DeviceStat{
    startTime: string;
    endTime: string;
  }
@Injectable({
  providedIn: 'root',
})

export class DevicesApi {
  constructor(private http: HttpClient) {}
  
  getDevices(): Observable<Device[]>{
    return this.http.get<Device[]>('https://localhost:7286/api/devices');
  }
 getDeviceStats(deviceId: string): Observable<DeviceStat[]>{
   return this.http.get<DeviceStat[]>(`https://localhost:7286/api/devices/${deviceId}/stats`);

 }
}
