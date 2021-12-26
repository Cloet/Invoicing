export abstract class BaseModel {

  protected _id: number = 0;


  public get id(): number {
      return this._id;
  }
  public set id(value: number) {
      this._id = value;
  }

}
