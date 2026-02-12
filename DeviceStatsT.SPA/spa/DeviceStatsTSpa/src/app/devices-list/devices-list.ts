import { Component, OnInit } from '@angular/core';
import { DevicesApi, Device } from '../devices-api';
import { NgFor, AsyncPipe } from '@angular/common';
import { Observable } from 'rxjs';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-devices-list',
  imports: [NgFor, AsyncPipe, RouterLink],
  templateUrl: './devices-list.html',
  styleUrl: './devices-list.less',
})
export class DevicesList implements OnInit {
  devices$!: Observable<Device[]>;

  constructor(private api: DevicesApi) {}

  ngOnInit() {
    this.devices$ = this.api.getDevices();
  }
}
