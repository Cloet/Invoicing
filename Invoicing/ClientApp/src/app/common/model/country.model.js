"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Country = void 0;
var Country = /** @class */ (function () {
    function Country(countryname, countrycode) {
        this.countryname = countryname;
        this.countrycode = countrycode;
        this.name = countryname;
        this.countryCode = countrycode;
    }
    Country.fromJson = function (other) {
        var country = new Country(other.name, other.countryCode);
        country._id = other.id;
        return country;
    };
    Country.prototype.toJSON = function () {
        return {
            id: this._id,
            name: this.name,
            countrycode: this._countryCode,
        };
    };
    Object.defineProperty(Country.prototype, "id", {
        get: function () {
            return this._id;
        },
        set: function (value) {
            this._id = value;
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(Country.prototype, "name", {
        get: function () {
            return this._name;
        },
        set: function (value) {
            this._name = value;
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(Country.prototype, "countryCode", {
        get: function () {
            return this._countryCode;
        },
        set: function (value) {
            this._countryCode = value;
        },
        enumerable: false,
        configurable: true
    });
    return Country;
}());
exports.Country = Country;
//# sourceMappingURL=country.model.js.map