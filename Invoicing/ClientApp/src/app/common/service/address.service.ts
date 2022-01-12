import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { BehaviorSubject } from "rxjs";
import { catchError, map, tap } from "rxjs/operators";
import { Address } from "../model/address.model";
import { BaseService } from "./base.service";

@Injectable({
  providedIn: 'root'
})
export class AddressService extends BaseService<Address> {

  public _addresses = new BehaviorSubject<Address[]>([]);

  private getAddressArray$(): Observable<Address[]> {
    return this._addresses.asObservable();
  }

  public getAddressForId$(addressId: number): Observable<Address> {
    const headers = this.defaultHeaders();
    const options = { headers };

    return this.http.get<Address>(`${this.serviceUrl}/address/${addressId}`, options)
      .pipe(
        catchError(
          error => {
            this.loadingError$.next(this.getErrorMsg(error));
            this.handleError(error);
            return of(error);
          }
        ),
        map(Address.fromJson)
      );
  }

  public getAddresses$(): Observable<Address[]> {
    const headers = this.defaultHeaders();
    const options = { headers };

    this.http.get(`${this.serviceUrl}/address/`, options)
      .pipe(
        catchError(
          error => {
            this.loadingError$.next(this.getErrorMsg(error));
            this.handleError(error);
            return of(error);
          }
        ),
        map((list: any[]): Address[] => list.map(Address.fromJson))
    ).subscribe(res => this._addresses.next(res));

    return this.getAddressArray$();
  }

  public createAddress$(address: Address): Observable<Address> {
    const headers = this.defaultHeaders();
    const options = { headers };
    const payload = JSON.stringify(address);

    return this.http.post<Address>(`${this.serviceUrl}/address/`, payload, options)
      .pipe(
        catchError(error => {
          this.postError$.next(this.getErrorMsg(error));
          this.handleError(error);
          return of(error);
        }),
        tap(x => this.addItemToArray(Address.fromJson(x), this._addresses)),
        map(Address.fromJson)
      );
  }

  public updateAddress$(address: Address): Observable<Address> {
    const headers = this.defaultHeaders();
    const options = { headers };
    const payload = JSON.stringify(address);

    return this.http.put(`${this.serviceUrl}/address/${address.id}`, payload, options)
      .pipe(
        catchError(
          error => {
            this.putError$.next(this.getErrorMsg(error));
            this.handleError(error);
            return of(error);
          }),
        tap(x => this.updateItemInArray(Address.fromJson(x), this._addresses)),
        map(Address.fromJson)
      );
  }

  deleteAddress(id: number) {
    const headers = this.defaultHeaders();
    const options = { headers };

    return this.http.delete(`${this.serviceUrl}/address/${id}`, options)
      .pipe(
        catchError(error => {
          this.deleteError$.next(this.getErrorMsg(error));
          this.handleError(error);
          return of(error);
        }),
        tap((res => this.removeFromArray(id, this._addresses)))
      );
  }




}
