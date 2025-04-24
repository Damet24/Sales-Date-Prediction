import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
;
import { Observable } from 'rxjs';
import { CreateOrderRequest } from '../interfaces/request-response';

@Injectable({
  providedIn: 'root',
})
export class OrderApiService {
  private http = inject(HttpClient);
  private baseUrl = 'http://localhost:5106';

  createOrder(order: CreateOrderRequest): Observable<any> {
    return this.http.post(`${this.baseUrl}/Orders`, order);
  }

  getEmployees(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Employees`);
  }

  getShippers(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Shippers`);
  }

  getProducts(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Products`);
  }
}
