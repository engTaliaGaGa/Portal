import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable()
export class ClientService {
  constructor(private http: HttpClient) {
  }

  getHeaders(data?: any[]): HttpHeaders {
    var headers: HttpHeaders = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    headers = headers.append('Access-Control-Allow-Origin', '*');
    if (environment.token != "") {
      headers = headers.append('Authorization', `Bearer ${environment.token}`);
    }
    if (data != null) {
      data.forEach(
        (item) => {
          let header = headers.get(item.Name);
          if (header == null) {
            headers = headers.append(item.Name, item.Value);
          } else if (item.Value == undefined) {
            headers = headers.delete(item.Name);
          } else {
            headers = headers.set(item.Name, item.Value);
          }
        }
      );
    }
    return headers;
  }

  public get<T>(serviceName: string, successCallback: any, errorCallback: any, data?: any[]) {
    this.http.get<T>(`${environment.serviceEndpoint}${serviceName}`, { headers: this.getHeaders(data), observe: 'response' })
      .subscribe(
        (response: any) => {
          let data: T = { ...response.body };
          if (successCallback != null) {
            successCallback(data);
          }
        },
        error => {
          if (errorCallback != null) {
            errorCallback(error);
          }
        }
      );
  }

  public getAsPromise<T>(serviceName: string, data?: any[]) {
    return this.http.get<T>(`${environment.serviceEndpoint}${serviceName}`, { headers: this.getHeaders(data), observe: 'response' }).toPromise();
  }

  public getAsObservable<T>(serviceName: string, data?: any[]): Observable<HttpResponse<T>> {
    return this.http.get<T>(`${environment.serviceEndpoint}${serviceName}`, { headers: this.getHeaders(data), observe: 'response' });
  }

  public post<U, T>(serviceName: string, body: U, successCallback: any, errorCallback: any, data?: any[]) {
    this.http.post<T>(`${environment.serviceEndpoint}${serviceName}`, body, { headers: this.getHeaders(data), observe: 'response' })
      .subscribe(
        (response: any) => {
          let data: T = { ...response.body };
          if (successCallback != null) {
            successCallback(data);
          }
        },
        error => {
          if (errorCallback != null) {
            errorCallback(error);
          }
        }
      );
  }

  public postAsPromise<U, T>(serviceName: string, body: U, data?: any[]) {
    return this.http.post<T>(`${environment.serviceEndpoint}${serviceName}`, body, { headers: this.getHeaders(data), observe: 'response' }).toPromise();
  }

  public postAsObservable<U, T>(serviceName: string, body: U, data?: any[]): Observable<HttpResponse<T>> {
    return this.http.post<T>(`${environment.serviceEndpoint}${serviceName}`, body, { headers: this.getHeaders(data), observe: 'response' });
  }

  public put<U, T>(serviceName: string, body: U, successCallback: any, errorCallback: any, data?: any[]) {
    this.http.put<T>(`${environment.serviceEndpoint}${serviceName}`, body, { headers: this.getHeaders(data), observe: 'response' })
      .subscribe(
        (response: any) => {
          let data: T = { ...response.body };
          if (successCallback != null) {
            successCallback(data);
          }
        },
        error => {
          if (errorCallback != null) {
            errorCallback(error);
          }
        }
      );
  }

  public putAsPromise<U, T>(serviceName: string, body: U, data?: any[]) {
    return this.http.put<T>(`${environment.serviceEndpoint}${serviceName}`, body, { headers: this.getHeaders(data), observe: 'response' }).toPromise();
  }

  public putAsObservable<U, T>(serviceName: string, body: U, data?: any[]): Observable<HttpResponse<T>> {
    return this.http.put<T>(`${environment.serviceEndpoint}${serviceName}`, body, { headers: this.getHeaders(data), observe: 'response' });
  }

  public delete<T>(serviceName: string, successCallback: any, errorCallback: any, data?: any[]) {
    this.http.delete<T>(`${environment.serviceEndpoint}${serviceName}`, { headers: this.getHeaders(data), observe: 'response' })
      .subscribe(
        (response: any) => {
          let data: T = { ...response.body };
          if (successCallback != null) {
            successCallback(data);
          }
        },
        error => {
          if (errorCallback != null) {
            errorCallback(error);
          }
        }
      );
  }

  public deleteAsPromise<T>(serviceName: string, data?: any[]) {
    return this.http.delete<T>(`${environment.serviceEndpoint}${serviceName}`, { headers: this.getHeaders(data), observe: 'response' }).toPromise();
  }

  public deleteAsObservable<T>(serviceName: string, data?: any[]): Observable<HttpResponse<T>> {
    return this.http.delete<T>(`${environment.serviceEndpoint}${serviceName}`, { headers: this.getHeaders(data), observe: 'response' });
  }
}
