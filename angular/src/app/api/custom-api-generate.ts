import {
  mergeMap as _observableMergeMap,
  catchError as _observableCatch,
} from 'rxjs/operators';
import {
  Observable,
  throwError as _observableThrow,
  of as _observableOf,
} from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpResponse,
  HttpResponseBase,
  HttpContext,
} from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({
  providedIn: 'root',
})
export class UserClientCustom {
  private http: HttpClient;
  private baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined =
    undefined;

  constructor(@Inject(HttpClient) http: HttpClient) {
    this.http = http;
    this.baseUrl = 'https://localhost:5001';
  }

  /**
   * @param body (optional)
   * @return Success
   */
  userPUT(id: string, body?: UpdateUserDto | undefined): Observable<void> {
    if (body === null || body === undefined) {
      throw new Error("The 'body' parameter must be defined.");
    }
    let url_ = this.baseUrl + '/api/User/{id}';
    if (id === undefined || id === null)
      throw new Error("The parameter 'id' must be defined.");
    url_ = url_.replace('{id}', encodeURIComponent('' + id));
    url_ = url_.replace(/[?&]$/, '');

    const content_ = new FormData();
    if (body.fullName === null || body.fullName === undefined) {
      throw new Error("The parameter 'fullName' cannot be null.");
    } else {
      content_.append('FullName', body.fullName.toString());
    }
    if (body.birthDay !== null && body.birthDay !== undefined) {
      content_.append('BirthDay', body.birthDay.toString());
    }
    //   if (body.address !== null || body.address !== undefined)
    //     content_.append('Address', body.address.toString());
    if (body.phoneNumber !== null && body.phoneNumber !== undefined) {
      content_.append('PhoneNumber', body.phoneNumber.toString());
    }
    if (body.fileImage !== null && body.fileImage !== undefined) {
      if (body.fileImage instanceof Blob) {
        content_.append(
          'FileImage',
          body.fileImage,
          body.fileImage.name ? body.fileImage.name : 'FileImage'
        );
      } else {
        // Xử lý trường hợp body.fileImage không phải là đối tượng Blob
        console.error('body.fileImage is not a Blob.');
      }
    }

    let options_: any = {
      body: content_,
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({}),
    };

    return this.http
      .request('put', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processUserPUT(response_);
        })
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processUserPUT(response_ as any);
            } catch (e) {
              return _observableThrow(e) as any as Observable<void>;
            }
          } else return _observableThrow(response_) as any as Observable<void>;
        })
      );
  }

  protected processUserPUT(response: HttpResponseBase): Observable<void> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse
        ? response.body
        : (response as any).error instanceof Blob
        ? (response as any).error
        : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    if (status === 200) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText: string) => {
          return _observableOf(null as any);
        })
      );
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText: string) => {
          return throwException(
            'An unexpected server error occurred.',
            status,
            _responseText,
            _headers
          );
        })
      );
    }
    return _observableOf(null as any);
  }
}

export interface UpdateUserDto {
  fullName?: string | undefined;
  birthDay?: Date | undefined;
  address?: string | undefined;
  phoneNumber?: string | undefined;
  fileImage?: any | undefined;
}

function blobToText(blob: any): Observable<string> {
  return new Observable<string>((observer: any) => {
    if (!blob) {
      observer.next('');
      observer.complete();
    } else {
      let reader = new FileReader();
      reader.onload = (event) => {
        observer.next((event.target as any).result);
        observer.complete();
      };
      reader.readAsText(blob);
    }
  });
}

function throwException(
  message: string,
  status: number,
  response: string,
  headers: { [key: string]: any },
  result?: any
): Observable<any> {
  if (result !== null && result !== undefined) return _observableThrow(result);
  else
    return _observableThrow(
      new ApiException(message, status, response, headers, null)
    );
}

export class ApiException extends Error {
  override message: string;
  status: number;
  response: string;
  headers: { [key: string]: any };
  result: any;

  constructor(
    message: string,
    status: number,
    response: string,
    headers: { [key: string]: any },
    result: any
  ) {
    super();

    this.message = message;
    this.status = status;
    this.response = response;
    this.headers = headers;
    this.result = result;
  }

  protected isApiException = true;

  static isApiException(obj: any): obj is ApiException {
    return obj.isApiException === true;
  }
}
