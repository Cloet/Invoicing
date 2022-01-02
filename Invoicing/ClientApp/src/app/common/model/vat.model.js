import { BaseModel } from "./base.model";
export class VAT extends BaseModel {
    constructor() {
        super();
        this.code = '';
        this.percentage = 0;
        this.description = '';
    }
    static fromJson(other) {
        const vat = new VAT();
        vat.id = other.id;
        vat.code = other.code;
        vat.percentage = other.percentage;
        vat.description = other.description;
        return vat;
    }
    toJSON() {
        return {
            id: this.id,
            code: this.code,
            percentage: this.percentage,
            description: this.description
        };
    }
}
//# sourceMappingURL=vat.model.js.map