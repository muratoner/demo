import { Component, AfterViewInit, ViewChild } from '@angular/core'
import { ServerDataSource, Ng2SmartTableModule } from 'ng2-smart-table'
import { ServerSourceConf } from 'ng2-smart-table/lib/data-source/server/server-source.conf'
import { environment } from '../../../../environments/environment'
import { NotificationService } from '../../../helper/notification.service';
import { HttpClientService } from '../../../helper/httpclient.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'ngx-smart-table',
  templateUrl: './smart-table.component.html',
  styleUrls: ['./smart-table.component.scss'],
})
export class SmartTableComponent implements AfterViewInit {

  @ViewChild('productTable', { static: true })
  productTable: any

  settings = {
    add: {
      addButtonContent: '<i class="nb-plus"></i>',
      createButtonContent: '<i class="nb-checkmark"></i>',
      cancelButtonContent: '<i class="nb-close"></i>',
      confirmCreate: true
    },
    edit: {
      editButtonContent: '<i class="nb-edit"></i>',
      saveButtonContent: '<i class="nb-checkmark"></i>',
      cancelButtonContent: '<i class="nb-close"></i>',
      confirmSave: true
    },
    delete: {
      deleteButtonContent: '<i class="nb-trash"></i>',
      confirmDelete: true,
    },
    columns: {
      id: {
        title: 'Id',
        type: 'number',
        width: '20px',
        editable: false
      },
      name: {
        title: 'Name',
        type: 'string',
      },
      price: {
        title: 'Price',
        type: 'string',
      },
      company: {
        title: 'Company',
        type: 'string',
      },
      mail: {
        title: 'E-mail',
        type: 'string',
      },
      color: {
        title: 'Color',
        type: 'string',
      },
      seller: {
        title: 'Seller',
        type: 'string',
      },
      currency: {
        title: 'Currency',
        type: 'string',
      },
      category: {
        title: 'Category',
        type: 'string',
      },
      ean13: {
        title: 'Ean13',
        type: 'string',
      }
    },
  }

  source: ServerDataSource
  sourceConf: ServerSourceConf

  constructor(private httpClient: HttpClientService, private http: HttpClient, private notification: NotificationService) {
    notification.loadingShow('Loading datas...')
    this.sourceConf = new ServerSourceConf({
      endPoint: `${environment.apiHost}/api/product`,
      totalKey: 'count',
      dataKey: 'data'
    })
    this.source = new ServerDataSource(this.http, this.sourceConf)
    this.source.onChanged().subscribe((res) => {
      notification.loadingHide()
    })
  }

  ngAfterViewInit(): void {
    ServerDataSource.prototype.find = function (element) {
      const found = this.data.find(function (el) { return el.id === element.id; });
      if (found) {
          return Promise.resolve(found);
      }
      return Promise.reject(new Error('Element was not found in the dataset'));
    };

    const that: any = this
    this.source.onUpdated().subscribe(function (data) {
      const changedRow = that.productTable.grid.dataSet.selectedRow;
      changedRow.setData(data);
    });
  }

  onDeleteConfirm(event): void {
    if (window.confirm('Are you sure you want to delete?')) {
      this.httpClient.delete(`/api/product/${event.data.id}`).then(res => {
        event.confirm.resolve()
      })
    } else {
      event.confirm.reject()
    }
  }

  onSaveConfirm(event): void {
    if (window.confirm('Are you sure you want to update record?')) {
      this.httpClient.putModel(`/api/product/${event.newData.id}`, JSON.stringify(event.newData)).then(res => {
        event.confirm.resolve()
      })
    } else {
      event.confirm.reject()
    }
  }

  onCreateConfirm(event): void {
    if (window.confirm('Are you sure you want to create record?')) {
      event.newData.id = 0;
      this.httpClient.postModel('/api/product', JSON.stringify(event.newData)).then(res => {
        if (!res.hasError) {
          event.confirm.resolve()
        }
      })
    } else {
      event.confirm.reject()
    }
  }
}
