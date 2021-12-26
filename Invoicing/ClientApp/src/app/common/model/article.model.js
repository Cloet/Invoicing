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
var vat_model_1 = require("./vat.model");
var Article = /** @class */ (function (_super) {
    __extends(Article, _super);
    function Article() {
        var _this = _super.call(this) || this;
        _this._articlecode = '';
        _this._description = '';
        _this._unitprice = 0;
        _this._vat = new vat_model_1.VAT();
        _this._unitpriceincludingvat = 0;
        return _this;
    }
    Article.fromJson = function (other) {
        var article = new Article();
        article.id = other.id;
        article.articlecode = other.articlecode;
        article.description = other.description;
        article.unitprice = other.unitprice;
        article.vat = other.vat;
        article.unitpriceincludingvat = other.unitpriceincludingvat;
        return article;
    };
    Article.prototype.toJSON = function () {
        return {
            id: this._id,
            articlecode: this._articlecode,
            description: this._description,
            unitprice: this._unitprice,
            vat: this._vat,
            unitpriceincludingvat: this._unitpriceincludingvat
        };
    };
    Object.defineProperty(Article.prototype, "articlecode", {
        get: function () {
            return this._articlecode;
        },
        set: function (value) {
            this._articlecode = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(Article.prototype, "description", {
        get: function () {
            return this._description;
        },
        set: function (value) {
            this._description = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(Article.prototype, "unitprice", {
        get: function () {
            return this._unitprice;
        },
        set: function (value) {
            this._unitprice = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(Article.prototype, "vat", {
        get: function () {
            return this._vat;
        },
        set: function (value) {
            this._vat = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(Article.prototype, "unitpriceincludingvat", {
        get: function () {
            return this._unitpriceincludingvat;
        },
        set: function (value) {
            this._unitpriceincludingvat = value;
        },
        enumerable: true,
        configurable: true
    });
    return Article;
}(base_model_1.BaseModel));
exports.Article = Article;
//# sourceMappingURL=article.model.js.map