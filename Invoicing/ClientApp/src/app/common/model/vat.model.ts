import { BaseModel } from "./base.model";

export class VAT extends BaseModel {

  private _code: string = '';
  private _percentage: number = 0;

  constructor() {
    super();
  }

  static fromJson(other: any): VAT {
    const vat = new VAT();
    vat.id = other.id;
    vat.code = other.code;
    vat.percentage = other.percentage;
    return vat;
  }

  toJSON(): any {
    return {
      id: this._id,
      code: this._code,
      percentage: this._percentage
    }
  }

  public get code(): string {
    return this._code;
  }

  public set code(value: string) {
    this._code = value;
  }

  public get percentage(): number {
    return this._percentage;
  }

  public set percentage(value: number) {
    this._percentage = value;
  }


}
