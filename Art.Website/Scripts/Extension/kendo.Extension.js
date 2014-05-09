kendo.data.binders.date = kendo.data.Binder.extend({
    init: function (element, bindings, options) {
        kendo.data.Binder.fn.init.call(this, element, bindings, options);
        this.yearControl = $("select:eq(0)", element);
        this.monthControl = $("select:eq(1)", element);
        this.dayControl = $("select:eq(2)", element);

        this._change = $.proxy(this.change, this);

        this.yearControl.on("change", this._change);
        this.monthControl.on("change", this._change);
        this.dayControl.on("change", this._change);
    },
    change: function () {
        var value = this.yearControl.val() + "-" + this.monthControl.val() + "-" + this.dayControl.val();
        this.bindings["date"].set(value);
    },
    refresh: function () {
        var value = this.bindings["date"].get();

        if (value) {
            var dateParts = value.split("-");
            this.yearControl.val(dateParts[0] * 1);
            this.monthControl.val(dateParts[1] * 1);
            this.dayControl.val(dateParts[2] * 1);
        }
    }
});
