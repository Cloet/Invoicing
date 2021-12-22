import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Observable } from 'rxjs';
import { of } from 'rxjs/internal/observable/of';
import { catchError, map, tap } from 'rxjs/operators';
import { Country } from '../model/country.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CountryService extends BaseService {

  public _countries = new BehaviorSubject<Country[]>([]);

  // Gets updated when items are created / updated.
  getCountriesArray$(): Observable<Country[]> {
    return this._countries.asObservable();
  }

  getCountryForId$(countryId: number): Observable<Country> {
    const headers = this.defaultHeaders();
    const options = { headers };

    return this.http.get<Country>(`${this.serviceUrl}/country/${countryId}`, options)
      .pipe(
        catchError(
          error => {
            this.loadingError$.next(error.error.message);
            this.handleError(error);
            return of(error);
        }),
        map(Country.fromJson)
      );
  }

  getCountries$(): Observable<Country[]> {
    const headers = this.defaultHeaders();
    const options = { headers };

    this.http.get(`${this.serviceUrl}/country/`, options)
      .pipe(
        catchError(
          error => {
            this.loadingError$.next(error.error.message);
            this.handleError(error);
            return of(error);
          }
        ),
        map((list: any[]): Country[] => list.map(Country.fromJson))
    ).subscribe(res => this._countries.next(res));

    return this._countries;
  }

  createCountry$(country: Country): Observable<Country> {
    const headers = this.defaultHeaders();
    const options = { headers };
    const payload = JSON.stringify(country);

    return this.http.post<Country>(`${this.serviceUrl}/country/`, payload, options)
      .pipe(
        catchError(error => {
          this.postError$.next(error.error.message);
          this.handleError(error);
          return of(error);
        }),
        tap(x => this.addCountry(Country.fromJson(x))),
        map(Country.fromJson)
      );
  }

  deleteCountry(id: number) {
    const headers = this.defaultHeaders();
    const options = { headers };

    return this.http.delete(`${this.serviceUrl}/country/${id}`, options)
      .pipe(
        catchError(error => {
          this.loadingError$.next(error.error.message);
          this.handleError(error);
          return of(error);
        }),
        tap((res => this.removeCountry(id)))
      );
  }

  private removeCountry(id: number) {
    this._countries.next(this._countries.value.filter(x => x.id !== id));
  }

  private addCountry(newCountry: Country) {
    let arrayCopy = this._countries.value;
    arrayCopy.push(newCountry);
    this._countries.next(arrayCopy);
  }

}
