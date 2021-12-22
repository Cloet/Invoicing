import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { of } from 'rxjs/internal/observable/of';
import { catchError, map } from 'rxjs/operators';
import { Country } from '../model/country.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CountryService extends BaseService {

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

    return this.http.get(`${this.serviceUrl}/country/`, options)
      .pipe(
        catchError(
          error => {
            this.handleError(error);
            this.loadingError$.next(error.error.message);
            return of(error);
          }
        ),
        map((list: any[]): Country[] => list.map(Country.fromJson))
      );
  }


}
