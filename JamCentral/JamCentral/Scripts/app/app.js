﻿var GigsController = function () {

    var button;

    var init = function () {
        $(".js-toggle-attendance").click(toggleAttendance);       
    };

    var toggleAttendance = function (e) {
        button = $(e.target);
        createAttendence();
    };

    var createAttendence = function () {
        $.post("/api/attendences", { GigId: button.attr("data-gig-id") })
            .done(done)
            .fail(fail);
    };

    var done = function () {
        button
            .removeClass("btn-light")
            .addClass("btn-info")
            .text("Going");
    };

    var fail = function () {
        alert("Something failed!");
    };

    return {
        init: init
    }
}();