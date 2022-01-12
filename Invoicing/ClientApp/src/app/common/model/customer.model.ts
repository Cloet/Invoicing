import { Address } from "./address.model";
import { BaseModel } from "./base.model";

export class Customer extends BaseModel {

  public customerCode: string = '';
  public name: string = '';
  public telephone: string = '';
  public mobile: string = '';
  public email: string = '';
  public website: string = '';
  public address: Address[] = [];

  constructor() {
    super();
  }

  static fromJson(other: any): Customer {
    const customer = new Customer();
    customer.customerCode = other.customerCode;
    customer.name = other.name;
    customer.telephone = other.telephone;
    customer.mobile = other.mobile;
    customer.email = other.email;
    customer.website = other.website;
    customer.address = other.address;
    return customer;
  }

  toJSON(): any {
    return {
      customerCode: this.customerCode,
      name: this.name,
      telephone: this.telephone,
      mobile: this.mobile,
      email: this.email,
      website: this.website,
      address: this.address
    }
  }

}
