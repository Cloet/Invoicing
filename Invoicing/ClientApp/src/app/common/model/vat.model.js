"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var base_model_1 = require("./base.model");
var VAT = /** @class */ (function (_super) {
    __extends(VAT, _super);
    function VAT() {
        var _this = _super.call(this) || this;
        _this._code = '';
        _this._percentage = 0;
        return _this;
    }
    VAT.fromJson = function (other) {
        var vat = new VAT();
        vat.id = other.id;
        vat.code = other.code;
        vat.percentage = other.percentage;
        return vat;
    };
    VAT.prototype.toJSON = function () {
        return {
            id: this._id,
            code: this._code,
            percentage: this._percentage
        };
    };
    Object.defineProperty(VAT.prototype, "code", {
        get: function () {
            return this._code;
        },
        set: function (value) {
            this._code = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(VAT.prototype, "percentage", {
        get: function () {
            return this._percentage;
        },
        set: function (value) {
            this._percentage = value;
        },
        enumerable: true,
        configurable: true
    });
    return VAT;
}(base_model_1.BaseModel));
exports.VAT = VAT;
//# sourceMappingURL=vat.model.js.map