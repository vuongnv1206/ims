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

export interface ISettingClient {
  /**
   * @param type (optional)
   * @param keyWords (optional)
   * @param page (optional)
   * @param itemsPerPage (optional)
   * @param skip (optional)
   * @param take (optional)
   * @param sortField (optional)
   * @return Success
   */
  settingGET(
    type?: SettingType | undefined,
    keyWords?: string | undefined,
    page?: number | undefined,
    itemsPerPage?: number | undefined,
    skip?: number | undefined,
    take?: number | undefined,
    sortField?: string | undefined
  ): Observable<SettingResponse>;
  /**
   * @param body (optional)
   * @return Success
   */
  settingPOST(body?: CreateUpdateSetting | undefined): Observable<SettingDto>;
  /**
   * @param body (optional)
   * @return Success
   */
  settingPUT(
    id: number,
    body?: CreateUpdateSetting | undefined
  ): Observable<SettingDto>;
  /**
   * @return Success
   */
  settingDELETE(id: number): Observable<void>;
  /**
   * @return Success
   */
  settingGET2(id: number): Observable<SettingDto>;
}

@Injectable({
  providedIn: 'root',
})
export class SettingClient implements ISettingClient {
  private http: HttpClient;
  private baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined =
    undefined;

  constructor(
    @Inject(HttpClient) http: HttpClient,
    @Optional() @Inject(API_BASE_URL) baseUrl?: string
  ) {
    this.http = http;
    this.baseUrl = 'https://localhost:5001';
  }

  /**
   * @param type (optional)
   * @param keyWords (optional)
   * @param page (optional)
   * @param itemsPerPage (optional)
   * @param skip (optional)
   * @param take (optional)
   * @param sortField (optional)
   * @return Success
   */
  settingGET(
    type?: SettingType | undefined,
    keyWords?: string | undefined,
    page?: number | undefined,
    itemsPerPage?: number | undefined,
    skip?: number | undefined,
    take?: number | undefined,
    sortField?: string | undefined,
    httpContext?: HttpContext
  ): Observable<SettingResponse> {
    let url_ = this.baseUrl + '/api/Setting?';
    if (type === null) throw new Error("The parameter 'type' cannot be null.");
    else if (type !== undefined)
      url_ += 'Type=' + encodeURIComponent('' + type) + '&';
    if (keyWords === null)
      throw new Error("The parameter 'keyWords' cannot be null.");
    else if (keyWords !== undefined)
      url_ += 'KeyWords=' + encodeURIComponent('' + keyWords) + '&';
    if (page === null) throw new Error("The parameter 'page' cannot be null.");
    else if (page !== undefined)
      url_ += 'Page=' + encodeURIComponent('' + page) + '&';
    if (itemsPerPage === null)
      throw new Error("The parameter 'itemsPerPage' cannot be null.");
    else if (itemsPerPage !== undefined)
      url_ += 'ItemsPerPage=' + encodeURIComponent('' + itemsPerPage) + '&';
    if (skip === null) throw new Error("The parameter 'skip' cannot be null.");
    else if (skip !== undefined)
      url_ += 'Skip=' + encodeURIComponent('' + skip) + '&';
    if (take === null) throw new Error("The parameter 'take' cannot be null.");
    else if (take !== undefined)
      url_ += 'Take=' + encodeURIComponent('' + take) + '&';
    if (sortField === null)
      throw new Error("The parameter 'sortField' cannot be null.");
    else if (sortField !== undefined)
      url_ += 'SortField=' + encodeURIComponent('' + sortField) + '&';
    url_ = url_.replace(/[?&]$/, '');

    let options_: any = {
      observe: 'response',
      responseType: 'blob',
      context: httpContext,
      headers: new HttpHeaders({
        Accept: 'text/plain',
      }),
    };

    return this.http
      .request('get', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processSettingGET(response_);
        })
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processSettingGET(response_ as any);
            } catch (e) {
              return _observableThrow(e) as any as Observable<SettingResponse>;
            }
          } else
            return _observableThrow(
              response_
            ) as any as Observable<SettingResponse>;
        })
      );
  }

  protected processSettingGET(
    response: HttpResponseBase
  ): Observable<SettingResponse> {
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
          let result200: any = null;
          result200 =
            _responseText === ''
              ? null
              : (JSON.parse(
                  _responseText,
                  this.jsonParseReviver
                ) as SettingResponse);
          return _observableOf(result200);
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

  /**
   * @param body (optional)
   * @return Success
   */
  settingPOST(
    body?: CreateUpdateSetting | undefined,
    httpContext?: HttpContext
  ): Observable<SettingDto> {
    let url_ = this.baseUrl + '/api/Setting';
    url_ = url_.replace(/[?&]$/, '');

    const content_ = JSON.stringify(body);

    let options_: any = {
      body: content_,
      observe: 'response',
      responseType: 'blob',
      context: httpContext,
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Accept: 'text/plain',
      }),
    };

    return this.http
      .request('post', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processSettingPOST(response_);
        })
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processSettingPOST(response_ as any);
            } catch (e) {
              return _observableThrow(e) as any as Observable<SettingDto>;
            }
          } else
            return _observableThrow(response_) as any as Observable<SettingDto>;
        })
      );
  }

  protected processSettingPOST(
    response: HttpResponseBase
  ): Observable<SettingDto> {
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
          let result200: any = null;
          result200 =
            _responseText === ''
              ? null
              : (JSON.parse(
                  _responseText,
                  this.jsonParseReviver
                ) as SettingDto);
          return _observableOf(result200);
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

  /**
   * @param body (optional)
   * @return Success
   */
  settingPUT(
    id: number,
    body?: CreateUpdateSetting | undefined,
    httpContext?: HttpContext
  ): Observable<SettingDto> {
    let url_ = this.baseUrl + '/api/Setting/{id}';
    if (id === undefined || id === null)
      throw new Error("The parameter 'id' must be defined.");
    url_ = url_.replace('{id}', encodeURIComponent('' + id));
    url_ = url_.replace(/[?&]$/, '');

    const content_ = JSON.stringify(body);

    let options_: any = {
      body: content_,
      observe: 'response',
      responseType: 'blob',
      context: httpContext,
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Accept: 'text/plain',
      }),
    };

    return this.http
      .request('put', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processSettingPUT(response_);
        })
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processSettingPUT(response_ as any);
            } catch (e) {
              return _observableThrow(e) as any as Observable<SettingDto>;
            }
          } else
            return _observableThrow(response_) as any as Observable<SettingDto>;
        })
      );
  }

  protected processSettingPUT(
    response: HttpResponseBase
  ): Observable<SettingDto> {
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
          let result200: any = null;
          result200 =
            _responseText === ''
              ? null
              : (JSON.parse(
                  _responseText,
                  this.jsonParseReviver
                ) as SettingDto);
          return _observableOf(result200);
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

  /**
   * @return Success
   */
  settingDELETE(id: number, httpContext?: HttpContext): Observable<void> {
    let url_ = this.baseUrl + '/api/Setting/{id}';
    if (id === undefined || id === null)
      throw new Error("The parameter 'id' must be defined.");
    url_ = url_.replace('{id}', encodeURIComponent('' + id));
    url_ = url_.replace(/[?&]$/, '');

    let options_: any = {
      observe: 'response',
      responseType: 'blob',
      context: httpContext,
      headers: new HttpHeaders({}),
    };

    return this.http
      .request('delete', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processSettingDELETE(response_);
        })
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processSettingDELETE(response_ as any);
            } catch (e) {
              return _observableThrow(e) as any as Observable<void>;
            }
          } else return _observableThrow(response_) as any as Observable<void>;
        })
      );
  }

  protected processSettingDELETE(response: HttpResponseBase): Observable<void> {
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

  /**
   * @return Success
   */
  settingGET2(id: number, httpContext?: HttpContext): Observable<SettingDto> {
    let url_ = this.baseUrl + '/api/Setting/{id}';
    if (id === undefined || id === null)
      throw new Error("The parameter 'id' must be defined.");
    url_ = url_.replace('{id}', encodeURIComponent('' + id));
    url_ = url_.replace(/[?&]$/, '');

    let options_: any = {
      observe: 'response',
      responseType: 'blob',
      context: httpContext,
      headers: new HttpHeaders({
        Accept: 'text/plain',
      }),
    };

    return this.http
      .request('get', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processSettingGET2(response_);
        })
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processSettingGET2(response_ as any);
            } catch (e) {
              return _observableThrow(e) as any as Observable<SettingDto>;
            }
          } else
            return _observableThrow(response_) as any as Observable<SettingDto>;
        })
      );
  }

  protected processSettingGET2(
    response: HttpResponseBase
  ): Observable<SettingDto> {
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
          let result200: any = null;
          result200 =
            _responseText === ''
              ? null
              : (JSON.parse(
                  _responseText,
                  this.jsonParseReviver
                ) as SettingDto);
          return _observableOf(result200);
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

export interface Setting {
  id?: number;
  creationTime?: Date | undefined;
  createdBy?: string | undefined;
  lastModificationTime?: Date | undefined;
  lastModifiedBy?: string | undefined;
  type?: SettingType;
  description?: string | undefined;
  name?: string | undefined;
  classes?: Class[] | undefined;
}

export interface CreateUpdateSetting {
  type?: SettingType;
  description?: string | undefined;
  name?: string | undefined;
}

export interface SettingDto {
  id?: number;
  type?: SettingType;
  description?: string | undefined;
  name?: string | undefined;
}

export interface SettingResponse {
  page?: PagingResponseInfo;
  settings?: SettingDto[] | undefined;
}

export enum SettingType {
  Semester = 1,
  Domain = 2,
}

export interface PagingResponseInfo {
  itemsPerPage?: number;
  currentPage?: number;
  toTalPage?: number;
  toTalRecord?: number;
}

export interface Class {
  id?: number;
  creationTime?: Date | undefined;
  createdBy?: string | undefined;
  lastModificationTime?: Date | undefined;
  lastModifiedBy?: string | undefined;
  name?: string | undefined;
  description?: string | undefined;
  subjectId?: number;
  settingId?: number;
  setting?: Setting;
  subject?: Subject;
  classStudents?: ClassStudent[] | undefined;
  milestones?: Milestone[] | undefined;
  projects?: Project[] | undefined;
  issueSettings?: IssueSetting[] | undefined;
}

export interface Subject {
  id?: number;
  creationTime?: Date | undefined;
  createdBy?: string | undefined;
  lastModificationTime?: Date | undefined;
  lastModifiedBy?: string | undefined;
  name?: string | undefined;
  description?: string | undefined;
  assignments?: Assignment[] | undefined;
  classes?: Class[] | undefined;
  issueSettings?: IssueSetting[] | undefined;
  subjectUsers?: SubjectUser[] | undefined;
}

export interface Assignment {
  id?: number;
  creationTime?: Date | undefined;
  createdBy?: string | undefined;
  lastModificationTime?: Date | undefined;
  lastModifiedBy?: string | undefined;
  name?: string | undefined;
  description?: string | undefined;
  subjectId?: number;
  subject?: Subject;
}

export interface SubjectUser {
  id?: number;
  creationTime?: Date | undefined;
  createdBy?: string | undefined;
  lastModificationTime?: Date | undefined;
  lastModifiedBy?: string | undefined;
  userId?: string;
  subjectId?: number;
  subject?: Subject;
  user?: AppUser;
}

export interface AppUser {
  id?: string;
  userName?: string | undefined;
  normalizedUserName?: string | undefined;
  email?: string | undefined;
  normalizedEmail?: string | undefined;
  emailConfirmed?: boolean;
  passwordHash?: string | undefined;
  securityStamp?: string | undefined;
  concurrencyStamp?: string | undefined;
  phoneNumber?: string | undefined;
  phoneNumberConfirmed?: boolean;
  twoFactorEnabled?: boolean;
  lockoutEnd?: Date | undefined;
  lockoutEnabled?: boolean;
  accessFailedCount?: number;
  fullName?: string | undefined;
  address?: string | undefined;
  avatar?: string | undefined;
  birthDay?: Date | undefined;
  creationTime?: Date | undefined;
  subjectUsers?: SubjectUser[] | undefined;
  issues?: Issues[] | undefined;
  classStudents?: ClassStudent[] | undefined;
  projectMembers?: ProjectMember[] | undefined;
}

export interface Issues {
  id?: number;
  creationTime?: Date | undefined;
  createdBy?: string | undefined;
  lastModificationTime?: Date | undefined;
  lastModifiedBy?: string | undefined;
  name?: string | undefined;
  description?: string | undefined;
  startDate?: Date | undefined;
  dueDate?: Date | undefined;
  assigneeId?: string;
  isOpen?: boolean;
  projectId?: number;
  issueSettingId?: number | undefined;
  milestoneId?: number;
  assignee?: AppUser;
  milestone?: Milestone;
  project?: Project;
  issueSetting?: IssueSetting;
  labels?: Label[] | undefined;
}

export interface ProjectMember {
  id?: number;
  creationTime?: Date | undefined;
  createdBy?: string | undefined;
  lastModificationTime?: Date | undefined;
  lastModifiedBy?: string | undefined;
  userId?: string;
  projectId?: number;
  user?: AppUser;
  project?: Project;
}

export interface Label {
  id?: number;
  creationTime?: Date | undefined;
  createdBy?: string | undefined;
  lastModificationTime?: Date | undefined;
  lastModifiedBy?: string | undefined;
  name?: string | undefined;
  issueId?: number;
  issues?: Issues;
}

export interface ClassStudent {
  id?: number;
  creationTime?: Date | undefined;
  createdBy?: string | undefined;
  lastModificationTime?: Date | undefined;
  lastModifiedBy?: string | undefined;
  userId?: string;
  classId?: number;
  class?: Class;
  user?: AppUser;
}

export interface Milestone {
  id?: number;
  creationTime?: Date | undefined;
  createdBy?: string | undefined;
  lastModificationTime?: Date | undefined;
  lastModifiedBy?: string | undefined;
  description?: string | undefined;
  startDate?: Date | undefined;
  dueDate?: Date | undefined;
  projectId?: number | undefined;
  project?: Project;
  classId?: number | undefined;
  class?: Class;
  issues?: Issues[] | undefined;
}

export interface Project {
  id?: number;
  creationTime?: Date | undefined;
  createdBy?: string | undefined;
  lastModificationTime?: Date | undefined;
  lastModifiedBy?: string | undefined;
  name?: string | undefined;
  description?: string | undefined;
  avatarUrl?: string | undefined;
  status?: number;
  classId?: number;
  class?: Class;
  projectMembers?: ProjectMember[] | undefined;
  milestones?: Milestone[] | undefined;
  issues?: Issues[] | undefined;
  issueSettings?: IssueSetting[] | undefined;
}

export interface IssueSetting {
  id?: number;
  creationTime?: Date | undefined;
  createdBy?: string | undefined;
  lastModificationTime?: Date | undefined;
  lastModifiedBy?: string | undefined;
  projectId?: number | undefined;
  classId?: number | undefined;
  subjectId?: number | undefined;
  project?: Project;
  subject?: Subject;
  class?: Class;
  issues?: Issues[] | undefined;
}
