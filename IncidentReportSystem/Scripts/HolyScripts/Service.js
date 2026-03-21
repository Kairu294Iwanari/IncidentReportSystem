app.service("IncidentReportSystemService", function ($http) {

    this.saveCurrentUser = function (user) {
        sessionStorage.setItem("CurrentUser", JSON.stringify(user));
    };

    this.getCurrentUser = function () {
        var data = sessionStorage.getItem("CurrentUser");
        return data ? JSON.parse(data) : null;
    };

    this.clearCurrentUser = function () {
        sessionStorage.removeItem("CurrentUser");
    };

});