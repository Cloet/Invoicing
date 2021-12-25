import { Country } from "./country.model";

export class City {

  private _id: number = 0;
  private _name: string = '';
  private _mainMunicipality: boolean = false;
  private _postal: string = '';
  private _country: Country | undefined;
    

  constructor() {
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
      id: this._id,
      name: this._name,
      mainmunicipality: this._mainMunicipality,
      postal: this._postal,
      country: this._country
    }
  }

  public get id(): number {
    return this._id;
  }
  public set id(value: number) {
    this._id = value;
  }

  public get name(): string {
    return this._name;
  }
  public set name(value: string) {
    this._name = value;
  }

  public get mainMunicipality(): boolean {
    return this._mainMunicipality;
  }
  public set mainMunicipality(value: boolean) {
    this._mainMunicipality = value;
  }

  public get postal(): string {
    return this._postal;
  }
  public set postal(value: string) {
    this._postal = value;
  }

  public get country(): Country | undefined {
    return this._country;
  }
  public set country(value: Country | undefined) {
    this._country = value;
  }

}
