app.service("IncidentReportSystemService", function ($http) {
    this.saveCurrentUser = function (user) { sessionStorage.setItem("CurrentUser", JSON.stringify(user)); };
    this.getCurrentUser = function () {
        var data = sessionStorage.getItem("CurrentUser");
        try { return data ? JSON.parse(data) : null; } catch (e) { return null; }
    };
    this.clearCurrentUser = function () { sessionStorage.removeItem("CurrentUser"); };

    this.loginService = function (loginData) { return $http({ url: "/Incident/LoginUser", method: "POST", data: JSON.stringify(loginData), headers: { "Content-Type": "application/json" } }); };
    this.UpsertService = function (userData) { return $http({ url: "/Incident/UpsertUsers", method: "POST", data: JSON.stringify(userData), headers: { "Content-Type": "application/json" } }); };

    this.GetStatusCardService = function () { return $http.get("/Incident/GetCardsStatus"); };
    this.getDashboardDataService = function () { return $http.get("/Incident/GetDashboardData"); };
    this.getDropdownDataService = function () { return $http.get("/Incident/GetDropdownData"); };
    this.getStatusListService = function () { return $http.get("/Incident/GetStatusList"); };
    this.getUserStatsService = function () { return $http.get("/Incident/GetUserStats"); };

    this.getAllUsersService = function () { return $http.get("/Incident/GetAllUsersList"); };
    this.adminUpdateUserService = function (userData) { return $http({ url: "/Incident/AdminUpdateUser", method: "POST", data: JSON.stringify(userData), headers: { "Content-Type": "application/json" } }); };
    this.adminDeleteUserService = function (userData) { return $http({ url: "/Incident/AdminDeleteUser", method: "POST", data: JSON.stringify(userData), headers: { "Content-Type": "application/json" } }); };

    this.getPieGraphService = function () { return $http.get("/Incident/GetPieGraph"); };
    this.getLineGraphService = function () { return $http.get("/Incident/GetLineGraph"); };
    this.getBarGraphService = function () { return $http.get("/Incident/GetBarGraph"); };

    this.submitIncidentService = function (incidentData) { return $http({ url: "/Incident/SubmitIncidentReport", method: "POST", data: JSON.stringify(incidentData), headers: { "Content-Type": "application/json" } }); };
    this.updateStatusService = function (updateData) { return $http({ url: "/Incident/UpdateIncidentStatus", method: "POST", data: JSON.stringify(updateData), headers: { "Content-Type": "application/json" } }); };
});