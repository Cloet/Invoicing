import { BaseModel } from "./base.model";
import { City } from "./city.model";
export class Address extends BaseModel {
    constructor() {
        super();
        this.name = '';
        this.number = '';
        this.street = '';
        this.city = new City();
    }
    static fromJson(other) {
        const address = new Address();
        address.id = other.id;
        address.name = other.name;
        address.number = other.number;
        address.street = other.street;
        address.city = other.city;
        return address;
    }
    toJSON() {
        return {
            id: this.id,
            name: this.name,
            number: this.number,
            street: this.street,
            city: this.city
        };
    }
}
//# sourceMappingURL=address.model.js.map