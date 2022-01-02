import { BaseModel } from "./base.model";
export class Country extends BaseModel {
    constructor() {
        super();
        this.name = '';
        this.countryCode = '';
    }
    static fromJson(other) {
        const country = new Country();
        country.name = other.name;
        country.countryCode = other.countryCode;
        country.id = other.id;
        return country;
    }
    toJSON() {
        return {
            id: this.id,
            name: this.name,
            countrycode: this.countryCode,
        };
    }
}
//# sourceMappingURL=country.model.js.map