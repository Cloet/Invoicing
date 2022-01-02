import { BaseModel } from "./base.model";
import { VAT } from "./vat.model";
export class Article extends BaseModel {
    constructor() {
        super();
        this.articlecode = '';
        this.description = '';
        this.unitprice = 0;
        this.vat = new VAT();
        this.unitpriceincludingvat = 0;
    }
    static fromJson(other) {
        const article = new Article();
        article.id = other.id;
        article.articlecode = other.articleCode;
        article.description = other.description;
        article.unitprice = other.unitPrice;
        article.vat = other.vat;
        article.unitpriceincludingvat = other.unitPriceIncludingVAT;
        return article;
    }
    toJSON() {
        return {
            id: this.id,
            articlecode: this.articlecode,
            description: this.description,
            unitprice: this.unitprice,
            vat: this.vat,
            unitpriceincludingvat: this.unitpriceincludingvat
        };
    }
}
//# sourceMappingURL=article.model.js.map