//Styles
import '../Styles/site.scss';
import 'daterangepicker/daterangepicker.css';

//libs
import 'bootstrap';

import { show_alert } from "./show_alert";

$("#confirmationModal").on('show.bs.modal', function (e) {
    let button = $(e.relatedTarget);
    let cancelUrl = window.location.origin + "/" + button.data("cancel-url");
    $(this).find("#confirm-cancel-button").click(function () {
        $.post(cancelUrl, {}, function (data) {
            if (data.success) {
                location.reload();
            } else {
                show_alert("Wystąpił błąd");
            }
        })
    })
});

$('#posterUpload').on('change', function () {
    let filename :string = $(this).val() as string;
    $('#posterUploadLabel').html(filename);
});

import 'daterangepicker';

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
