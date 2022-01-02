export class BaseModel {
    constructor() {
        this.id = 0;
    }
    updatePartial(init) {
        Object.assign(this, init);
    }
}
//# sourceMappingURL=base.model.js.map