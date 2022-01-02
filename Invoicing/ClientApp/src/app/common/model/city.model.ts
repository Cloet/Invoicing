import { BaseModel } from "./base.model";
import { Country } from "./country.model";

export class City extends BaseModel {

  public name: string = '';
  public mainMunicipality: boolean = false;
  public postal: string = '';
  public country: Country = new Country();
    

  constructor() {
    super();
  }

  static fromJson(other: any): City {
    const city = new City();

    city.id = other.id;
    city.name = other.name;
    city.mainMunicipality = other.mainMunicipality;
    city.postal = other.postal;
    city.country = other.country;

    return city;
  }

  toJSON(): any {
    return {
      id: this.id,
      name: this.name,
      mainmunicipality: this.mainMunicipality,
      postal: this.postal,
      country: this.country
    }
  }

}
