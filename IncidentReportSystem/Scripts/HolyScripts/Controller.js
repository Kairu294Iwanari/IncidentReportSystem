app.controller("IncidentReportSystemController", function ($scope, $timeout, $interval, IncidentReportSystemService) {

    $scope.statusLabels = [];
    $scope.statusData = [];
    $scope.chartOptions = { responsive: true, legend: { display: true, position: 'bottom' } };

    $scope.showLogin = false;
    $scope.currentUser = null;
    $scope.cardData = [];
    $scope.dashboardDataArray = [];
    $scope.categoriesList = [];
    $scope.prioritiesList = [];
    $scope.statusList = [];
    $scope.selectedIncident = null;

    $scope.submitted = false;
    $scope.reportSubmitted = false;
    $scope.showReportForm = false;

    $scope.loginUsername = "";
    $scope.loginPassword = "";
    var autoUpdateTimer;

    $scope.adminActiveTab = 'users';
    $scope.userStats = { Total: 0, Residents: 0, Officials: 0 };

    $scope.rolesList = [
        { RoleID: 1, RoleName: 'Resident' },
        { RoleID: 2, RoleName: 'Barangay Official' }
    ];

    $scope.initialRun = function () {
        $scope.currentUser = IncidentReportSystemService.getCurrentUser();
        var currentPath = window.location.pathname.toLowerCase();

        if (currentPath.includes("dashboard")) {
            if (!$scope.currentUser) { window.location.href = "/Incident/LoginPage"; return; }
            $scope.getIncidentCardStatus();
            $scope.fetchDashboardData();
            $scope.fetchDropdowns();
            if ($scope.currentUser.RoleID === 3) {
                $scope.fetchUserStats();
            }
            $scope.startAutoUpdate();
        }
    };

    $scope.startAutoUpdate = function () {
        autoUpdateTimer = $interval(function () {
            $scope.getIncidentCardStatus();
            $scope.fetchDashboardData();
            if ($scope.currentUser && $scope.currentUser.RoleID === 3) {
                $scope.fetchUserStats();
            }
        }, 5000);
    };

    $scope.$on('$destroy', function () {
        if (autoUpdateTimer) $interval.cancel(autoUpdateTimer);
    });

    $scope.setAdminTab = function (tabName) {
        $scope.adminActiveTab = tabName;
    };

    $scope.fetchUserStats = function () {
        var getStats = IncidentReportSystemService.getUserStatsService();
        getStats.then(function (response) {
            if (response && response.data && !response.data.error) {
                $scope.userStats = response.data;
            }
        });
    };

    $scope.fetchDashboardData = function () {
        var getData = IncidentReportSystemService.getDashboardDataService();
        getData.then(function (response) {
            if (response && response.data && !response.data.error) {
                var formattedData = response.data.map(function (item) {
                    if (item.incident.CreatedAt && typeof item.incident.CreatedAt === "string" && item.incident.CreatedAt.indexOf("/Date") !== -1) {
                        item.incident.CreatedAt = parseInt(item.incident.CreatedAt.replace(/[^0-9]/g, ""));
                    }
                    return item;
                });

                $scope.dashboardDataArray = formattedData;

                if ($scope.currentUser && $scope.currentUser.RoleID === 3) {
                    var statusMap = {};
                    formattedData.forEach(function (rep) {
                        var statusName = rep.status.StatusName;
                        statusMap[statusName] = (statusMap[statusName] || 0) + 1;
                    });
                    $scope.statusLabels = Object.keys(statusMap);
                    $scope.statusData = Object.values(statusMap);
                }
            }
        });
    };

    $scope.getIncidentCardStatus = function () {
        var getData = IncidentReportSystemService.GetStatusCardService();
        getData.then(function (returnedData) {
            if (returnedData && returnedData.data) { $scope.cardData = returnedData.data; }
        });
    };

    $scope.toggleReportForm = function () {
        $scope.showReportForm = !$scope.showReportForm;
        if (!$scope.showReportForm) $scope.clearReportFields();
    };

    $scope.submitReportFunc = function () {
        $scope.reportSubmitted = true;
        if (!$scope.incidentTitle || !$scope.incidentLocation || !$scope.incidentDesc || !$scope.incidentCategory || !$scope.incidentPriority) {
            Swal.fire({ title: "Missing Fields", text: "Please fill in all report details.", icon: "warning" }); return;
        }

        var reportData = {
            ResidentID: $scope.currentUser.UserID, Title: $scope.incidentTitle, Location: $scope.incidentLocation,
            Description: $scope.incidentDesc, CategoryID: parseInt($scope.incidentCategory), PriorityID: parseInt($scope.incidentPriority)
        };

        var submitData = IncidentReportSystemService.submitIncidentService(reportData);
        submitData.then(function (returnedData) {
            if (returnedData.data === "Success") {
                Swal.fire({ title: "Report Submitted!", text: "Your incident has been sent.", icon: "success" });
                $scope.clearReportFields(); $scope.showReportForm = false;
                $scope.fetchDashboardData(); $scope.getIncidentCardStatus();
            } else { Swal.fire({ title: "Failed", text: returnedData.data, icon: "error" }); }
        });
    };

    $scope.viewIncident = function (data) {
        $scope.selectedIncident = angular.copy(data);
        $scope.selectedIncident.incident.StatusID = $scope.selectedIncident.incident.StatusID.toString();
        if ($scope.statusList.length === 0) {
            var getStatusList = IncidentReportSystemService.getStatusListService();
            getStatusList.then(function (response) { if (response.data && !response.data.error) $scope.statusList = response.data; });
        }
    };

    $scope.closeIncidentView = function () { $scope.selectedIncident = null; };

    $scope.updateStatusFunc = function () {
        var updateData = { IncidentID: $scope.selectedIncident.incident.IncidentID, StatusID: parseInt($scope.selectedIncident.incident.StatusID) };
        var request = IncidentReportSystemService.updateStatusService(updateData);
        request.then(function (response) {
            if (response.data === "Success") {
                Swal.fire({ title: "Updated!", text: "Status has been changed.", icon: "success" });
                $scope.fetchDashboardData(); $scope.getIncidentCardStatus();
                $scope.selectedIncident = null;
            } else { Swal.fire({ title: "Failed", text: response.data, icon: "error" }); }
        });
    };

    $scope.loginFunc = function () {
        if (!$scope.loginUsername || !$scope.loginPassword) { Swal.fire({ title: "Missing Fields", icon: "warning" }); return; }
        var loginData = { Username: $scope.loginUsername, PasswordHash: $scope.loginPassword };
        var loginRequest = IncidentReportSystemService.loginService(loginData);

        loginRequest.then(function (response) {
            if (response.data.success) {
                IncidentReportSystemService.saveCurrentUser(response.data.userData);
                Swal.fire({ title: "Welcome!", icon: "success", timer: 1500, showConfirmButton: false }).then(function () {
                    if (response.data.userData.RoleID === 1) window.location.href = "/Incident/ResidentDashboard";
                    else if (response.data.userData.RoleID === 2) window.location.href = "/Incident/OfficialDashboard";
                    else if (response.data.userData.RoleID === 3) window.location.href = "/Incident/AdminDashboard";
                });
            } else { Swal.fire({ title: "Login Failed", text: response.data.message, icon: "error" }); }
        });
    };

    $scope.UpsertFunc = function () {
        $scope.submitted = true;
        if (!$scope.FirstName || !$scope.LastName || !$scope.Username || !$scope.Pname || !$scope.ConfirmPass || !$scope.Role || !$scope.Phonenumber || !$scope.Address) { Swal.fire({ title: "Fields not completed", icon: "warning" }); return; }
        if ($scope.Pname !== $scope.ConfirmPass) { Swal.fire({ title: "Mismatch", text: "Passwords do not match.", icon: "warning" }); return; }

        var userData = { FirstName: $scope.FirstName, MiddleName: $scope.MiddleName || "", LastName: $scope.LastName, Username: $scope.Username, PasswordHash: $scope.Pname, PhoneNumber: $scope.Phonenumber, Address: $scope.Address, RoleID: parseInt($scope.Role), AccountStatusID: 1 };
        var upsertData = IncidentReportSystemService.UpsertService(userData);
        upsertData.then(function (returnedData) {
            if (returnedData.data === "Success") {
                Swal.fire({ title: "Account Created!", icon: "success" });
                $scope.clearFields(); $scope.showLogin = true;
            } else { Swal.fire({ title: "Registration Failed", text: returnedData.data, icon: "error" }); }
        });
    };

    $scope.logoutFunc = function () {
        Swal.fire({ title: "Are you sure?", text: "You will be logged out.", icon: "question", showCancelButton: true, confirmButtonColor: "#d33" }).then(function (result) {
            if (result.isConfirmed) { IncidentReportSystemService.clearCurrentUser(); window.location.href = "/Incident/LoginPage"; }
        });
    };

    $scope.fetchDropdowns = function () {
        var getDropdowns = IncidentReportSystemService.getDropdownDataService();
        getDropdowns.then(function (response) {
            if (response && response.data && !response.data.error) { $scope.categoriesList = response.data.categories; $scope.prioritiesList = response.data.priorities; }
        });
    };

    $scope.clearFields = function () { $scope.FirstName = ""; $scope.MiddleName = ""; $scope.LastName = ""; $scope.Username = ""; $scope.Pname = ""; $scope.ConfirmPass = ""; $scope.Phonenumber = ""; $scope.Address = ""; $scope.Role = ""; $scope.submitted = false; };
    $scope.clearReportFields = function () { $scope.incidentTitle = ""; $scope.incidentLocation = ""; $scope.incidentDesc = ""; $scope.incidentCategory = ""; $scope.incidentPriority = ""; $scope.reportSubmitted = false; };
});