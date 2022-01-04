import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, of } from 'rxjs';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Country } from '../model/country.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CountryService extends BaseService<Country> {

  private _countries = new BehaviorSubject<Country[]>([]);

  // Gets updated when items are created / updated.
  private getCountriesArray$(): Observable<Country[]> {
    return this._countries.asObservable();
  }

  public getCountryForId$(countryId: number): Observable<Country> {
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

  public getCountries$(): Observable<Country[]> {
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

    return this.getCountriesArray$();
  }

  public createCountry$(country: Country): Observable<Country> {
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
        tap(x => this.addItemToArray(Country.fromJson(x), this._countries)),
        map(Country.fromJson)
      );
  }

  public updateCountry$(country: Country): Observable<Country> {
    const headers = this.defaultHeaders();
    const options = { headers };
    const payload = JSON.stringify(country);

    return this.http.put(`${this.serviceUrl}/country/${country.id}`, payload, options)
      .pipe(
        catchError(error => {
          this.putError$.next(error.error.message);
          this.handleError(error);
          return of(error);
        }),
        tap(x => this.updateItemInArray(Country.fromJson(x), this._countries)),
        map(Country.fromJson)
      );

  }
    
  public deleteCountry(id: number) {
    const headers = this.defaultHeaders();
    const options = { headers };

    return this.http.delete(`${this.serviceUrl}/country/${id}`, options)
      .pipe(
        catchError(error => {
          this.deleteError$.next(error.error.message);
          this.handleError(error);
          return of(error);
        }),
        tap((res => this.removeFromArray(id, this._countries)))
      );
  }

}
