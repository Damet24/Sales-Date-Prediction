import { HttpClient } from '@angular/common/http';
import { computed, inject, Injectable, signal } from '@angular/core';
import { CustomerWithOrderDate } from '../interfaces/request-response';

interface State {
  orders: CustomerWithOrderDate[]
  loading: boolean
}

@Injectable({
  providedIn: 'root'
})
export class OrderActivityService {

  private http = inject(HttpClient)

  #state = signal<State>({
    loading: true,
    orders: []
  })

  orders = computed(() => this.#state().orders)
  loading = computed(() => this.#state().loading)

  constructor() {
    this.http.get<CustomerWithOrderDate[]>("http://localhost:5106/Customers/order-activity")
      .subscribe(res => {
        this.#state.set({
          loading: false,
          orders: res
        })
      })
  }
}
