"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var BaseModel = /** @class */ (function () {
    function BaseModel() {
        this._id = 0;
    }
    Object.defineProperty(BaseModel.prototype, "id", {
        get: function () {
            return this._id;
        },
        set: function (value) {
            this._id = value;
        },
        enumerable: true,
        configurable: true
    });
    return BaseModel;
}());
exports.BaseModel = BaseModel;
//# sourceMappingURL=base.model.js.map