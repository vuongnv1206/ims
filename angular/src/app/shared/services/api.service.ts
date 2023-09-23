import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

const prefixApi = '/api';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  /**
   *
   */
  public loading: BehaviorSubject<boolean> = new BehaviorSubject(false);

  constructor(private httpClient: HttpClient) {}
  public buildQueryParam(params: any) {
    let query = new HttpParams();
    if (params) {
      for (let k in params) {
        query = query.set(k, params[k]);
      }
    }
    return query.toString();
  }

  public loadingOn(): void {
    this.loading.next(true);
  }

  public get(url: string, query?: any, option?: any): Observable<any> {
    let urlFinal = url;
    if (query) {
      urlFinal = url + '?' + this.buildQueryParam(query);
    }
    this.loadingOn();
    return this.httpClient.get(
      'https://localhost:5001' + prefixApi + urlFinal,
      option
    );
  }

  /**
   * @description POST HTTP
   * @param url: string
   * @param body: any
   * @param option?: any
   * @return Observable
   */
  public post(url: string, body: any, option?: any): Observable<any> {
    this.loadingOn();
    return this.httpClient.post(
      'https://localhost:5001' + prefixApi + url,
      body,
      option
    );
  }
}
