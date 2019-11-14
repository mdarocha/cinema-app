"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
//Styles
require("../Styles/site.scss");
require("daterangepicker/daterangepicker.css");
//libs
require("bootstrap");
var show_alert_1 = require("./show_alert");
$("#confirmationModal").on('show.bs.modal', function (e) {
    var button = $(e.relatedTarget);
    var cancelUrl = button.data("cancel-url");
    $(this).find("#confirm-cancel-button").click(function () {
        $.post(cancelUrl, {}, function (data) {
            if (data.success) {
                location.reload();
            }
            else {
                show_alert_1.show_alert("Wystąpił błąd");
            }
        });
    });
});
$('#posterUpload').on('change', function () {
    var filename = $(this).val();
    $('#posterUploadLabel').html(filename);
});
require("daterangepicker");
$("#time-picker").daterangepicker({
    singleDatePicker: true,
    timePicker: true,
    timePicker24Hour: true,
    timePickerIncrement: 5,
    locale: {
        format: 'DD.MM.YY HH:mm',
        daysOfWeek: [
            "Nd",
            "Pn",
            "Wr",
            "Śr",
            "Cw",
            "Pt",
            "Sb"
        ],
        monthNames: [
            "Styczeń",
            "Luty",
            "Marzec",
            "Kwiecień",
            "Maj",
            "Czerwiec",
            "Lipiec",
            "Sierpień",
            "Wrzesień",
            "Październik",
            "Listopad",
            "Grudzień"
        ],
        firstDay: 1
    }
});
//# sourceMappingURL=app.js.map