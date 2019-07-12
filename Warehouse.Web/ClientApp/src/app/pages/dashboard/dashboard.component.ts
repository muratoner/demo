import {Component, OnDestroy} from '@angular/core';
import { HttpClientService } from '../../helper/httpclient.service';

@Component({
  selector: 'ngx-dashboard',
  styleUrls: ['./dashboard.component.scss'],
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnDestroy {
  model = {
    productsCount: 0
  }
  constructor(private http: HttpClientService) {
    this.http.get('/api/product?_page=0&_limit=0').then((res: any) => {
      this.model.productsCount = res.count
    })
  }

  ngOnDestroy() {
  }
}
