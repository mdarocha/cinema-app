//TODO replace this with something less hacky

$(document).ready(function () {
    let form = $('form')[0];
    let settings = $.data(form).validator.settings

    // Store existing event handlers in local variables
    let oldErrorPlacement = settings.errorPlacement
    let oldSuccess = settings.success;

    settings.errorPlacement = function (label, element) {
        // Add proper Bootstrap classes to input
        element.addClass('is-invalid');
        oldErrorPlacement(label, element);
    };

    settings.success = function (label) {
        // Remove Bootstrap classes
        element.removeClass('is-invalid');
        oldSuccess(label);
    };
});