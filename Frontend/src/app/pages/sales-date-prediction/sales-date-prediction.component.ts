import { Component, inject, ViewChild } from '@angular/core';
import { TableModule, Table } from 'primeng/table';
import { OrderActivityService } from '../../services/order.service';
import { ButtonModule } from 'primeng/button';
import { IconField } from 'primeng/iconfield';
import { InputIcon } from 'primeng/inputicon';
import { DatePipe } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { ToolbarModule } from 'primeng/toolbar';

@Component({
  selector: 'app-sales-date-prediction',
  imports: [TableModule, ButtonModule, IconField, InputIcon, DatePipe, InputTextModule, ToolbarModule],
  templateUrl: './sales-date-prediction.component.html',
  styleUrl: './sales-date-prediction.component.css'
})
export class SalesDatePredictionComponent {
  public ordersService = inject(OrderActivityService)
  @ViewChild('dt') dt: Table | undefined;

  applyFilterGlobal($event: any, stringVal: any) {
    this.dt!.filterGlobal(($event.target as HTMLInputElement).value, stringVal);
  }
}
