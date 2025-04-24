import { computed, inject, Injectable, signal } from '@angular/core';
import { CustomerOrders, Order } from '../interfaces/request-response';
import { HttpClient } from '@angular/common/http';

interface State {
  orders: Order[],
  customerName: string
  loading: boolean
}

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private http = inject(HttpClient)

  #state = signal<State>({
    loading: true,
    orders: [],
    customerName: ''
  })

  orders = computed(() => this.#state().orders)
  loading = computed(() => this.#state().loading)
  customerName = computed(() => this.#state().customerName)

  loadOrders(customerId: string) {
    this.#state.set({ loading: true, orders: [], customerName: '' });

    this.http.get<CustomerOrders>(`http://localhost:5106/Orders/${customerId}`)
      .subscribe(res => {
        this.#state.set({
          loading: false,
          orders: res.orders,
          customerName: res.customerName
        });
      });
  }
}
