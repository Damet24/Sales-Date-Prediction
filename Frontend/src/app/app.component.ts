import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { SalesDatePredictionComponent } from './pages/sales-date-prediction/sales-date-prediction.component';

@Component({
  selector: 'app-root',
  imports: [ButtonModule, SalesDatePredictionComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Frontend';
}
