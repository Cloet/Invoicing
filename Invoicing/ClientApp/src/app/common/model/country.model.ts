import { BaseModel } from "./base.model";

export class Country extends BaseModel {

  private _name: string = '';
  private _countryCode: string = '';
    
  constructor() {
    super();
  }

  static fromJson(other: any): Country {
    const country = new Country();
    country.name = other.name;
    country.countryCode = other.countryCode;
    country.id = other.id;
    return country;
  }

  toJSON(): any {
    return {
      id: this._id,
      name: this.name,
      countrycode: this._countryCode,
    }
  }
  

  public get name(): string {
    return this._name;
  }
  public set name(value: string) {
    this._name = value;
  }

  public get countryCode(): string {
    return this._countryCode;
  }
  public set countryCode(value: string) {
    this._countryCode = value;
  }

}
