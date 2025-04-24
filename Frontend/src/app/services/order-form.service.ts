import { Injectable, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { CreateOrderRequest } from '../interfaces/request-response';
import { OrderApiService } from './order-api.service';
import { forkJoin } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class OrderFormService {
  private fb = inject(FormBuilder);
  private orderApiService = inject(OrderApiService)

  employees = signal<{ id: number, fullName: string }[]>([]);
  shippers = signal<{ id: number, companyName: string }[]>([]);
  products = signal<{ id: number, productName: string }[]>([]);

  constructor() {
    this.loadDropdownData();
  }

  loadDropdownData() {
    forkJoin({
      employees: this.orderApiService.getEmployees(),
      shippers: this.orderApiService.getShippers(),
      products: this.orderApiService.getProducts()
    }).subscribe(({ employees, shippers, products }) => {
      this.employees.set(employees);
      this.shippers.set(shippers);
      this.products.set(products);
    });
  }

  createForm(): FormGroup {
    return this.fb.group({
      customerId: [null, [Validators.required, Validators.min(1)]],
      employeeId: [null, [Validators.required, Validators.min(1)]],
      shipperId: [null, [Validators.required, Validators.min(1)]],
      shipName: ['', Validators.required],
      shipAddress: ['', Validators.required],
      shipCity: ['', Validators.required],
      shipRegion: [''],
      shipPostalCode: [''],
      shipCountry: ['', Validators.required],
      orderDate: [null, Validators.required],
      requiredDate: [null, Validators.required],
      shippedDate: [null, Validators.required],
      freight: [null, Validators.required],
      orderDetails: this.createOrderDetail()
    });
  }

  createOrderDetail(): FormGroup {
    return this.fb.group({
      productId: [null, [Validators.required, Validators.min(1)]],
      quantity: [1, Validators.required],
      unitPrice: [0, Validators.required],
      discount: [0]
    });
  }

  createOrder(orderForm: FormGroup) {
    const payload = orderForm.value;
    payload.orderDetails = [payload.orderDetails]
    return this.orderApiService.createOrder(payload);
  }

  getOrderDetailsArray(form: FormGroup): FormArray {
    return form.get('orderDetails') as FormArray;
  }
}