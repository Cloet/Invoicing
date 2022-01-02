import { BaseModel } from "./base.model";
import { VAT } from "./vat.model";

export class Article extends BaseModel {

  public articlecode: string = '';
  public description: string = '';
  public unitprice: number = 0;
  public vat: VAT = new VAT();
  public unitpriceincludingvat: number = 0;

  constructor() {
    super();
  }

  static fromJson(other: any): Article {
    const article = new Article();

    article.id = other.id;
    article.articlecode = other.articleCode;
    article.description = other.description;
    article.unitprice = other.unitPrice;
    article.vat = other.vat;
    article.unitpriceincludingvat = other.unitPriceIncludingVAT;

    return article;
  }

  toJSON(): any {
    return {
      id: this.id,
      articlecode: this.articlecode,
      description: this.description,
      unitprice: this.unitprice,
      vat: this.vat,
      unitpriceincludingvat: this.unitpriceincludingvat
    }
  }

}
