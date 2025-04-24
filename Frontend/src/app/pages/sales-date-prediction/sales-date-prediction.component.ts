import { Component, inject, ViewChild } from '@angular/core';
import { TableModule, Table } from 'primeng/table';
import { CusotmerOrderActivityService } from '../../services/customer-order.service';
import { ButtonModule } from 'primeng/button';
import { IconField } from 'primeng/iconfield';
import { InputIcon } from 'primeng/inputicon';
import { DatePipe } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { ToolbarModule } from 'primeng/toolbar';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-sales-date-prediction',
  imports: [TableModule, ButtonModule, IconField, InputIcon, DatePipe, InputTextModule, ToolbarModule, RouterModule],
  templateUrl: './sales-date-prediction.component.html',
  styleUrl: './sales-date-prediction.component.css'
})
export class SalesDatePredictionComponent {
  public ordersService = inject(CusotmerOrderActivityService)
  router = inject(Router)
  route = inject(ActivatedRoute)

  @ViewChild('dt') dt: Table | undefined;

  applyFilterGlobal($event: any, stringVal: any) {
    this.dt!.filterGlobal(($event.target as HTMLInputElement).value, stringVal);
  }

  viewOrder(id: number) {
    this.router.navigate(
      [{ outlets: { modal: ['orders', id] } }],
      { relativeTo: this.route }
    );
  }

  newOrder(id: number) {
    this.router.navigate(
      [{ outlets: { modal: ['orders', 'new', id] } }],
      { relativeTo: this.route }
    );
  }
}
