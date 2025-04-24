import { Routes } from '@angular/router';


export const routes: Routes = [
    {
        path: "sales-date-prediction",
        loadComponent: () =>
            import('./pages/sales-date-prediction/sales-date-prediction.component')
                .then(m => m.SalesDatePredictionComponent),
        children: [
            {
                path: "orders/new/:customerId",
                outlet: "modal",
                loadComponent: () =>
                    import('./pages/new-order/new-order.component').then(m => m.NewOrderComponent)
            },
            {
                path: "orders/:customerId",
                outlet: "modal",
                loadComponent: () =>
                    import('./pages/orders/orders.component').then(m => m.OrdersComponent)
            }
        ]
    },
    {
        path: "",
        redirectTo: "sales-date-prediction",
        pathMatch: "full"
    }
];
