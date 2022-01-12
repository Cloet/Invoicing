import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, of } from "rxjs";
import { catchError, map, tap } from "rxjs/operators";
import { Customer } from "../model/customer.model";
import { BaseService } from "./base.service";

@Injectable({
  providedIn: 'root'
})
export class CustomerService extends BaseService<Customer> {

  public _customers = new BehaviorSubject<Customer[]>([]);

  private getCustomersArray$(): Observable<Customer[]> {
    return this._customers.asObservable();
  }

  public getCustomerForId$(customerId: number): Observable<Customer> {
    const headers = this.defaultHeaders();
    const options = { headers };

    return this.http.get<Customer>(`${this.serviceUrl}/customer/${customerId}`, options)
      .pipe(
        catchError(error => {
          this.loadingError$.next(this.getErrorMsg(error));
          this.handleError(error);
          return of(error);
        }),
        map(Customer.fromJson)
      )
  }

  public getCustomers$(): Observable<Customer[]> {
    const headers = this.defaultHeaders();
    const options = { headers };

    this.http.get(`${this.serviceUrl}/customer/`, options)
      .pipe(
        catchError(
          error => {
            this.loadingError$.next(this.getErrorMsg(error));
            this.handleError(error);
            return of(error);
          }
        ),
        map((list: any[]): Customer[] => list.map(Customer.fromJson))
    ).subscribe(res => this._customers.next(res));

    return this.getCustomersArray$();
  }

  public createCustomer$(customer: Customer): Observable<Customer> {
    const headers = this.defaultHeaders();
    const options = { headers };
    const payload = JSON.stringify(customer);

    return this.http.post<Customer>(`${this.serviceUrl}/customer`, payload, options)
      .pipe(
        catchError(error => {
          this.postError$.next(this.getErrorMsg(error));
          this.handleError(error);
          return of(error);
        }),
        tap(x => this.addItemToArray(Customer.fromJson(x), this._customers)),
        map(Customer.fromJson)
      );
  }

  public updateCustomer$(customer: Customer): Observable<Customer> {
    const headers = this.defaultHeaders();
    const options = { headers };
    const payload = JSON.stringify(customer);

    return this.http.put(`${this.serviceUrl}/customer/${customer.id}`, payload, options)
      .pipe(
        catchError(error => {
          this.putError$.next(this.getErrorMsg(error));
          this.handleError(error);
          return of(error);
        }),
        tap(x => this.updateItemInArray(Customer.fromJson(x), this._customers)),
        map(Customer.fromJson)
      );
  }

  deleteCustomer(id: number) {
    const headers = this.defaultHeaders();
    const options = { headers };

    return this.http.delete(`${this.serviceUrl}/customer/${id}`, options)
      .pipe(
        catchError(error => {
          this.deleteError$.next(this.getErrorMsg(error));
          this.handleError(error);
          return of(error);
        }),
        tap((res => this.removeFromArray(id, this._customers)))
      );
  }

}
