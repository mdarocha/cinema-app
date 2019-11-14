function show_alert(text: string) {
    const animation_duration = 200;

    let alert = $("<div></div>")
        .addClass("navbar-alert alert alert-danger")
        .css({ top: "0%" })
        .html(text);

    $("#reservation-flow-alerts").append(alert);

    alert.animate({ top: "100%" }, {
        duration: animation_duration,
        complete: () => {
            alert.delay(1200).animate({ top: "0%" }, {
                duration: animation_duration,
                complete: () => { alert.remove(); }
            });
        },
    });
}

export { show_alert };