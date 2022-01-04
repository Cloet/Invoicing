import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { of } from 'rxjs';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { City } from '../model/city.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CityService extends BaseService<City> {

  public _cities = new BehaviorSubject<City[]>([]);

  // Gets updated when items are created / updated.
  private getCitiesArray$(): Observable<City[]> {
    return this._cities.asObservable();
  }

  public getCityForId$(cityId: number): Observable<City> {
    const headers = this.defaultHeaders();
    const options = { headers };

    return this.http.get<City>(`${this.serviceUrl}/city/${cityId}`, options)
      .pipe(
        catchError(
          error => {
            this.loadingError$.next(error.error.message);
            this.handleError(error);
            return of(error);
          }),
        map(City.fromJson)
      );
  }

  public getCities$(): Observable<City[]> {
    const headers = this.defaultHeaders();
    const options = { headers };

    this.http.get(`${this.serviceUrl}/city/`, options)
      .pipe(
        catchError(
          error => {
            this.loadingError$.next(error.error.message);
            this.handleError(error);
            return of(error);
          }
        ),
        map((list: any[]): City[] => list.map(City.fromJson))
      ).subscribe(res => this._cities.next(res));

    return this.getCitiesArray$();
  }

  public createCity$(city: City): Observable<City> {
    const headers = this.defaultHeaders();
    const options = { headers };
    const payload = JSON.stringify(city);

    return this.http.post<City>(`${this.serviceUrl}/city/`, payload, options)
      .pipe(
        catchError(error => {
          this.postError$.next(error.error.message);
          this.handleError(error);
          return of(error);
        }),
        tap(x => this.addItemToArray(City.fromJson(x), this._cities)),
        map(City.fromJson)
      );
  }

  public updateCity$(city: City): Observable<City> {
    const headers = this.defaultHeaders();
    const options = { headers };
    const payload = JSON.stringify(city);

    return this.http.put(`${this.serviceUrl}/city/${city.id}`, payload, options)
      .pipe(
        catchError(error => {
          this.putError$.next(error.error.message);
          this.handleError(error);
          return of(error);
        }),
        tap(x => this.updateItemInArray(City.fromJson(x),this._cities)),
        map(City.fromJson)
      );

  }

  public deleteCity(id: number) {
    const headers = this.defaultHeaders();
    const options = { headers };

    return this.http.delete(`${this.serviceUrl}/city/${id}`, options)
      .pipe(
        catchError(error => {
          this.deleteError$.next(error.error.message);
          this.handleError(error);
          return of(error);
        }),
        tap((res => this.removeFromArray(id, this._cities)))
      );
  }

}
