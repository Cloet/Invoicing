import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Observable } from 'rxjs';
import { of } from 'rxjs/internal/observable/of';
import { catchError, map, tap } from 'rxjs/operators';
import { City } from '../model/city.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CityService extends BaseService {

  public _cities = new BehaviorSubject<City[]>([]);

  // Gets updated when items are created / updated.
  getCitiesArray$(): Observable<City[]> {
    return this._cities.asObservable();
  }

  getCityForId$(cityId: number): Observable<City> {
    const headers = this.defaultHeaders();
    const options = { headers };

    return this.http.get<City>(`${this.serviceUrl}/city/${cityId}`, options)
      .pipe(
        catchError(
          error => {
            this.loadingError$.next(error.error);
            this.handleError(error);
            return of(error);
          }),
        map(City.fromJson)
      );
  }

  getCities$(): Observable<City[]> {
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

    return this._cities;
  }

  createCity$(city: City): Observable<City> {
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
        tap(x => this.addCity(City.fromJson(x))),
        map(City.fromJson)
      );
  }

  updateCity$(city: City): Observable<City> {
    const headers = this.defaultHeaders();
    const options = { headers };
    const payload = JSON.stringify(City);

    return this.http.put(`${this.serviceUrl}/City/${city.id}`, payload, options)
      .pipe(
        catchError(error => {
          this.putError$.next(error.error.message);
          this.handleError(error);
          return of(error);
        }),
        tap(x => this.updateCity(City.fromJson(x))),
        map(City.fromJson)
      );

  }

  deleteCity(id: number) {
    const headers = this.defaultHeaders();
    const options = { headers };

    return this.http.delete(`${this.serviceUrl}/city/${id}`, options)
      .pipe(
        catchError(error => {
          this.loadingError$.next(error.error.message);
          this.handleError(error);
          return of(error);
        }),
        tap((res => this.removeCity(id)))
      );
  }

  private updateCity(city: City) {
    if (city.id === undefined || city.id <= 0)
      return;

    let arrayCopy = this._cities.value;
    const index = arrayCopy.findIndex(item => item.id === city.id);
    arrayCopy[index] = city;
    this._cities.next(arrayCopy);
  }

  private removeCity(id: number) {
    if (id === undefined || id <= 0)
      return;

    this._cities.next(this._cities.value.filter(x => x.id !== id));
  }

  private addCity(newCity: City) {
    if (newCity.id === undefined || newCity.id <= 0)
      return;

    let arrayCopy = this._cities.value;
    arrayCopy.push(newCity);
    this._cities.next(arrayCopy);
  }

}
