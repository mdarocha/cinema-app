"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function show_alert(text) {
    var animation_duration = 200;
    var alert = $("<div></div>")
        .addClass("navbar-alert alert alert-danger")
        .css({ top: "0%" })
        .html(text);
    $("#reservation-flow-alerts").append(alert);
    alert.animate({ top: "100%" }, {
        duration: animation_duration,
        complete: function () {
            alert.delay(1200).animate({ top: "0%" }, {
                duration: animation_duration,
                complete: function () { alert.remove(); }
            });
        },
    });
}
exports.show_alert = show_alert;
//# sourceMappingURL=show_alert.js.map