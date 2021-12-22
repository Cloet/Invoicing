import { Injectable } from '@angular/core';
import { Subject, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IError } from '../model/errors.model';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  protected serviceUrl = `${environment.backendApi}`;
  public postError$ = new Subject<string>();
  public loadingError$ = new Subject<string>();
  public putError$ = new Subject<string>();

  constructor(protected http: HttpClient) { }

  protected defaultHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json'
    });
  }

  protected handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occured:', error.error.message);
    } else {
      console.error(`Backend returned code ${error.status}, body was : ${error.error}`);
    }

    return throwError('Something bad happend; please try again later.');
  }

}
