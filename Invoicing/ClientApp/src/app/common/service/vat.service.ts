import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, of } from "rxjs";
import { catchError, map, tap } from "rxjs/operators";
import { VAT } from "../model/vat.model";
import { BaseService } from "./base.service";

@Injectable({
  providedIn: 'root'
})
export class VATService extends BaseService<VAT> {

  private _vat = new BehaviorSubject<VAT[]>([]);


  private getVATArray$(): Observable<VAT[]> {
    return this._vat.asObservable();
  }

  public getVATForId$(vatId: number): Observable<VAT> {
    const headers = this.defaultHeaders();
    const options = { headers };

    return this.http.get<VAT>(`${this.serviceUrl}/vat/${vatId}`, options)
      .pipe(
        catchError(error => {
          this.loadingError$.next(error.error.message);
          this.handleError(error);
          return of(error);
        }),
        map(VAT.fromJson)
      );
  }

  public getVAT$(): Observable<VAT[]> {
    const headers = this.defaultHeaders();
    const options = { headers };

    this.http.get(`${this.serviceUrl}/vat/`, options)
      .pipe(
        catchError(
          error => {
            this.loadingError$.next(error.error.message);
            this.handleError(error);
            return of(error);
          }
        ),
        map((list: any[]): VAT[] => list.map(VAT.fromJson))
      ).subscribe(res => this._vat.next(res));

    return this._vat;
  }

  public createVAT$(vat: VAT): Observable<VAT> {
    const headers = this.defaultHeaders();
    const options = { headers };
    const payload = JSON.stringify(vat);

    return this.http.post<VAT>(`${this.serviceUrl}/vat/`, payload, options)
      .pipe(
        catchError(error => {
          this.postError$.next(error.error.message);
          this.handleError(error);
          return of(error);
        }),
        tap(x => this.addItemToArray(VAT.fromJson(x), this._vat)),
        map(VAT.fromJson)
      );
  }

  public updateVAT$(vat: VAT): Observable<VAT> {
    const headers = this.defaultHeaders();
    const options = { headers };
    const payload = JSON.stringify(vat);

    return this.http.put(`${this.serviceUrl}/vat/${vat.id}`, payload, options)
      .pipe(
        catchError(error => {
          this.putError$.next(error.error.message);
          this.handleError(error);
          return of(error);
        }),
        tap(x => this.updateItemInArray(VAT.fromJson(x), this._vat)),
        map(VAT.fromJson)
      );
  }

  public deleteVAT(id: number) {
    const headers = this.defaultHeaders();
    const options = { headers };

    return this.http.delete(`${this.serviceUrl}/vat/${id}`, options)
      .pipe(
        catchError(error => {
          this.deleteError$.next(error.error.message);
          this.handleError(error);
          return of(error);
        }),
        tap((res => this.removeFromArray(id, this._vat)))
      );
  }

}
