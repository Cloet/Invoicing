import { BaseModel } from "./base.model";
import { City } from "./city.model";

export class Address extends BaseModel {

  public name: string = '';
  public number: string = '';
  public street: string = '';
  public city: City = new City();


  constructor() {
    super();
  }

  static fromJson(other: any): Address {
    const address = new Address();
    address.id = other.id;
    address.name = other.name;
    address.number = other.number;
    address.street = other.street;
    address.city = other.city;
    return address;
  }

  toJSON(): any {
    return {
      id: this.id,
      name: this.name,
      number: this.number,
      street: this.street,
      city: this.city
    }
  }

}
