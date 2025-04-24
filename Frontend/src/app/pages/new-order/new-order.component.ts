import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogModule } from 'primeng/dialog';
import { Select } from 'primeng/select';
import { InputTextModule } from 'primeng/inputtext';
import { DatePickerModule } from 'primeng/datepicker';
import { InputNumberModule } from 'primeng/inputnumber';
import { MessageService } from 'primeng/api';
import { DividerModule } from 'primeng/divider';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { OrderFormService } from '../../services/order-form.service';
import { Button } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { Message } from 'primeng/message';

@Component({
  selector: 'app-new-order',
  standalone: true,
  imports: [CommonModule, DialogModule, Select, 
    InputTextModule, DatePickerModule, 
    InputNumberModule, DividerModule, Button, 
    ReactiveFormsModule, Message],
  templateUrl: './new-order.component.html',
  styleUrls: ['./new-order.component.css']
})
export class NewOrderComponent {
  router = inject(Router)
  route = inject(ActivatedRoute)
  visible = true
  private orderFormService = inject(OrderFormService);
  orderForm: FormGroup = this.orderFormService.createForm();

  employees = this.orderFormService.employees;
  shippers = this.orderFormService.shippers;
  products = this.orderFormService.products;

  // Mensaje global de error
  formErrorMessage: string | null = null;
  messageService = inject(MessageService)

  constructor() {
    const customerId = this.route.snapshot.paramMap.get('customerId');
    if (customerId) {
      this.orderForm.patchValue({
        customerId: Number(customerId)
      });
    }
  }

  getEmployees() {
    return this.employees().map(item => item.fullName)
  }

  addDetail() {
    this.orderFormService.getOrderDetailsArray(this.orderForm).push(
      this.orderFormService.createOrderDetail()
    );
  }

  removeDetail(index: number) {
    this.orderFormService.getOrderDetailsArray(this.orderForm).removeAt(index);
  }

  submit() {
    if (this.orderForm.valid) {
      this.orderFormService.createOrder(this.orderForm).subscribe({
        next: () => {
          this.messageService.add({
            severity: 'success',
            summary: 'Orden creada',
            detail: 'La orden fue registrada correctamente'
          });
          this.closeModal();
        },
        error: (err) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'No se pudo crear la orden'
          });
          console.error('Error creando orden', err);
        }
      });
    } else {
      this.messageService.add({
        severity: 'warn',
        summary: 'Formulario inválido',
        detail: 'Por favor llena todos los campos requeridos'
      });
    }
  }

  closeModal() {
    this.router.navigate([{ outlets: { modal: null } }], {
      relativeTo: this.route.parent
    });
  }
}
