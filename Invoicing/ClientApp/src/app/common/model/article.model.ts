import { BaseModel } from "./base.model";
import { VAT } from "./vat.model";

export class Article extends BaseModel {

  private _articlecode: string = '';
  private _description: string = '';
  private _unitprice: number = 0;
  private _vat: VAT = new VAT();
  private _unitpriceincludingvat: number = 0;

  constructor() {
    super();
  }

  static fromJson(other: any): Article {
    const article = new Article();

    article.id = other.id;
    article.articlecode = other.articlecode;
    article.description = other.description;
    article.unitprice = other.unitprice;
    article.vat = other.vat;
    article.unitpriceincludingvat = other.unitpriceincludingvat;

    return article;
  }

  toJSON(): any {
    return {
      id: this._id,
      articlecode: this._articlecode,
      description: this._description,
      unitprice: this._unitprice,
      vat: this._vat,
      unitpriceincludingvat: this._unitpriceincludingvat
    }
  }

  public get articlecode(): string {
    return this._articlecode;
  }
  public set articlecode(value: string) {
    this._articlecode = value;
  }

  public get description(): string {
    return this._description;
  }
  public set description(value: string) {
    this._description = value;
  }

  public get unitprice(): number {
    return this._unitprice;
  }
  public set unitprice(value: number) {
    this._unitprice = value;
  }

  public get vat(): VAT {
    return this._vat;
  }
  public set vat(value: VAT) {
    this._vat = value;
  }

  public get unitpriceincludingvat(): number {
    return this._unitpriceincludingvat;
  }
  public set unitpriceincludingvat(value: number) {
    this._unitpriceincludingvat = value;
  }

}
