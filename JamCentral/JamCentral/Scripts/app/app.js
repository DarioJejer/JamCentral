function initGigs() {
    $(".js-toggle-attendance").click(function (e) {
        var button = $(e.target);
        $.post("/api/attendences", { GigId: button.attr("data-gig-id") })
            .done(function () {
                button
                    .removeClass("btn-light")
                    .addClass("btn-info")
                    .text("Going");
            })
            .fail(function () {
                alert("Something failed!");
            });
    });
}