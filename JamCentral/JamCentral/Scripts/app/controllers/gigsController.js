var GigsController = function (attendanceService) {

    var button;

    var init = function () {
        $(".js-toggle-attendance").click(toggleAttendance);
    };

    var toggleAttendance = function (e) {
        button = $(e.target);
        gigId = button.attr("data-gig-id")
        attendanceService.createAttendance(gigId, done, fail);
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
}(AttendanceService);