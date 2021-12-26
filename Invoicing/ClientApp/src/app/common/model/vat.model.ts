import { BaseModel } from "./base.model";

export class VAT extends BaseModel {

  private _code: string = '';
  private _percentage: number = 0;
  private _description: string = '';

  constructor() {
    super();
  }

  static fromJson(other: any): VAT {
    const vat = new VAT();
    vat.id = other.id;
    vat.code = other.code;
    vat.percentage = other.percentage;
    vat.description = other.description;
    return vat;
  }

  toJSON(): any {
    return {
      id: this._id,
      code: this._code,
      percentage: this._percentage,
      description: this._description
    }
  }

  public get code(): string {
    return this._code;
  }

  public set code(value: string) {
    this._code = value;
  }

  public get description(): string {
    return this._description;
  }

  public set description(value: string) {
    this._description = value;
  }

  public get percentage(): number {
    return this._percentage;
  }

  public set percentage(value: number) {
    this._percentage = value;
  }


}
