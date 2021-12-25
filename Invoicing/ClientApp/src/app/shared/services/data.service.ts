import { Observable } from "rxjs";
import { Subject } from "rxjs";

export abstract class DataService<T> {

  private _data$ = new Subject<T>();

  constructor() {

  }

  public getData$(): Observable<T> {
    return this._data$.asObservable();
  }

  public updateData(data: T): void {
    this._data$.next(data);
  }

}
