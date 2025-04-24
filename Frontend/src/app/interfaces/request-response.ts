
export interface CustomerWithOrderDate {
    id: number
    customerName: string
    lastOrderDate: Date
    nextPredictedOrder: Date
}

export interface CustomerOrders {
    customerName: string
    orders: Order[]
}
export interface Order {
    id: number
    requiredDate: string
    shippedDate: string
    shipperName: string
    shipperAddress: string
    shipperCity: string
}

export interface CreateOrderRequest {
    customerId: number;
    employeeId: number;
    shipperId: number;
    shipName: string;
    shipAddress: string;
    shipCity: string;
    shipRegion: string;
    shipPostalCode: string;
    orderDate: string;
    requiredDate: string;
    shippedDate: string;
    freight: number;
    shipCountry: string;
    orderDetails: OrderDetailRequest[];
  }
  
  export interface OrderDetailRequest {
    productId: number;
    quantity: number;
    unitPrice: number;
    discount: number;
  }