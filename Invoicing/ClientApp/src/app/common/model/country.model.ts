import { BaseModel } from "./base.model";

export class Country extends BaseModel {

  public name: string = '';
  public countryCode: string = '';
    
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
      id: this.id,
      name: this.name,
      countrycode: this.countryCode,
    }
  }


}
