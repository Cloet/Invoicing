export abstract class BaseModel {

  public id: number = 0;

  constructor() {
  }

  updatePartial<T>(init?: Partial<T>): void {
    Object.assign(this, init);
  }

}
