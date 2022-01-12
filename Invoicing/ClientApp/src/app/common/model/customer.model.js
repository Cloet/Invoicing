import { BaseModel } from "./base.model";
export class Customer extends BaseModel {
    constructor() {
        super();
        this.customerCode = '';
        this.name = '';
        this.telephone = '';
        this.mobile = '';
        this.email = '';
        this.website = '';
        this.address = [];
    }
    static fromJson(other) {
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
    toJSON() {
        return {
            customerCode: this.customerCode,
            name: this.name,
            telephone: this.telephone,
            mobile: this.mobile,
            email: this.email,
            website: this.website,
            address: this.address
        };
    }
}
//# sourceMappingURL=customer.model.js.map