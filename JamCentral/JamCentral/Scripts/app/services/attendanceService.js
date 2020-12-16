var AttendanceService = function () {

    var createAttendance = function (gigId, done, fail) {
        $.post("/api/attendences", { GigId: gigId })
            .done(done)
            .fail(fail);
    };

    return {
        createAttendance: createAttendance
    }
}();