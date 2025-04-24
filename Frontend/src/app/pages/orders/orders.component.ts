import { Component, inject } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { CustomerOrderTableComponent } from '../../components/customer-order-table/customer-order-table.component';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-orders',
  imports: [DialogModule, CustomerOrderTableComponent],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent {
  router = inject(Router)
  route = inject(ActivatedRoute)
  visible = true
  customerName = '';

  closeModal() {
    this.router.navigate([{ outlets: { modal: null } }], {
      relativeTo: this.route.parent
    });
  }


  setCustomerName(name: string) {
    this.customerName = name;
  }
}
