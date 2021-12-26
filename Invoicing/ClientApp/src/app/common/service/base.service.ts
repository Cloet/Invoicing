import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IError } from '../model/errors.model';
import { BaseModel } from '../model/base.model';

@Injectable({
  providedIn: 'root'
})
export abstract class BaseService<T extends BaseModel> {

  protected serviceUrl = `${environment.backendApi}`;
  public postError$ = new Subject<string>();
  public loadingError$ = new Subject<string>();
  public putError$ = new Subject<string>();
  public deleteError$ = new Subject<string>();

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

  protected updateItemInArray(item: T, array: BehaviorSubject<T[]>) {
    if (item.id === undefined || item.id <= 0)
      return;

    let arrayCopy = array.value;
    const index = arrayCopy.findIndex(x => x.id === item.id);
    arrayCopy[index] = item;
    array.next(arrayCopy);
  }

  protected removeFromArray(id: number, array: BehaviorSubject<T[]>) {
    if (id === undefined || id <= 0)
      return;

    array.next(array.value.filter(x => x.id !== id));
  }

  protected addItemToArray(newItem: T, array: BehaviorSubject<T[]>) {
    if (newItem.id === undefined || newItem.id <= 0)
      return;

    let arrayCopy = array.value;
    arrayCopy.push(newItem);
    array.next(arrayCopy);
  }


}
