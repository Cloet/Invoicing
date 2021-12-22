
export class Country {

  private _id: number = 0;
  private _name: string = '';
  private _countryCode: string = '';
    
  constructor() {
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

  public get countryCode(): string {
    return this._countryCode;
  }
  public set countryCode(value: string) {
    this._countryCode = value;
  }

}
