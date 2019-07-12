import { Injectable } from '@angular/core'
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { environment } from '../../environments/environment'
import { NotificationService } from './notification.service';

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
}

@Injectable()
export class HttpClientService {
    constructor(private httpClient: HttpClient, private notification: NotificationService) { }

    private ifShowHasError<T>(res: Promise<T>) {
        (<Promise<any>>res).then(_ => {
            if (_.hasError) {
                this.notification.dialogDanger('Error', _.errors)
            }
        })
    }

    private postAndPut(type: 'post' | 'put', url: string, model: any): Promise<any> {
        const res = this.httpClient[type](`${environment.apiHost}${url}`, model, httpOptions).toPromise();
        this.ifShowHasError(res)
        return res
    }

    get<T>(url: string): Promise<T> {
        const res = <Promise<T>>this.httpClient.get(`${environment.apiHost}${url}`).toPromise()
        this.ifShowHasError(res)
        return res
    }

    postBasic(url: string, model: any): Promise<any> {
        return this.postAndPut('post', url, model)
    }

    postModel<TReturnModel>(url: string, model: any): Promise<any> {
        return this.postAndPut('post', url, model)
    }

    putBasic(url: string, model: any): Promise<any> {
        return this.postAndPut('put', url, model)
    }

    putModel<TReturnModel>(url: string, model: any): Promise<any> {
        return this.postAndPut('put', url, model)
    }

    delete(url: string): Promise<any> {
        return <Promise<any>>this.httpClient.delete(`${environment.apiHost}${url}`, httpOptions).toPromise()
    }
}