import { BaseModel } from "./base.model";
import { Country } from "./country.model";
export class City extends BaseModel {
    constructor() {
        super();
        this.name = '';
        this.mainMunicipality = false;
        this.postal = '';
        this.country = new Country();
    }
    static fromJson(other) {
        const city = new City();
        city.id = other.id;
        city.name = other.name;
        city.mainMunicipality = other.mainMunicipality;
        city.postal = other.postal;
        city.country = other.country;
        return city;
    }
    toJSON() {
        return {
            id: this.id,
            name: this.name,
            mainmunicipality: this.mainMunicipality,
            postal: this.postal,
            country: this.country
        };
    }
}
//# sourceMappingURL=city.model.js.map