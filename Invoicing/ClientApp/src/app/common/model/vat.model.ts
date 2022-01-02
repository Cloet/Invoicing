import { BaseModel } from "./base.model";

export class VAT extends BaseModel {

  public code: string = '';
  public percentage: number = 0;
  public description: string = '';

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
      id: this.id,
      code: this.code,
      percentage: this.percentage,
      description: this.description
    }
  }


}
