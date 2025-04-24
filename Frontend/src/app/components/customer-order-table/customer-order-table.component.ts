import { DatePipe } from '@angular/common';
import { Component, EventEmitter, inject, Output, ViewChild, OnInit, effect } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { IconField } from 'primeng/iconfield';
import { InputIcon } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { Table, TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { OrderService } from '../../services/order.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-customer-order-table',
  imports: [TableModule, ButtonModule, IconField, InputIcon, DatePipe, InputTextModule, ToolbarModule],
  templateUrl: './customer-order-table.component.html',
  styleUrl: './customer-order-table.component.css'
})
export class CustomerOrderTableComponent {
  route = inject(ActivatedRoute)
  public ordersService = inject(OrderService)

  @Output() customerName = new EventEmitter<string>();

  @ViewChild('dt') dt: Table | undefined;

  applyFilterGlobal($event: any, stringVal: any) {
    this.dt!.filterGlobal(($event.target as HTMLInputElement).value, stringVal);
  }

  customerNameEffect = effect(() => {
    const name = this.ordersService.customerName();
    if (name) this.customerName.emit(name);
  });

  ngOnInit() {
    const customerId = this.route.snapshot.paramMap.get('customerId');
    if (customerId) {
      this.ordersService.loadOrders(customerId);
    }
  }
}
