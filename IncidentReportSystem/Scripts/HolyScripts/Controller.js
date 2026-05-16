app.controller("IncidentReportSystemController", function ($scope, $timeout, IncidentReportSystemService) {

    $scope.chartOptionsPie = { responsive: true, legend: { display: true, position: 'bottom' } };
    $scope.chartOptionsLineBar = {
        responsive: true,
        legend: { display: true, position: 'bottom' },
        scales: { yAxes: [{ ticks: { beginAtZero: true, stepSize: 1 } }] },
        elements: { line: { tension: 0.3 }, point: { radius: 4, hoverRadius: 6 } }
    };

    $scope.pieLabels = []; $scope.pieData = []; $scope.pieColors = [];
    $scope.lineLabels = []; $scope.lineSeries = []; $scope.lineData = []; $scope.lineColors = [];
    $scope.barLabels = []; $scope.barSeries = []; $scope.barData = []; $scope.barColors = [];

    $scope.showLogin = false;
    $scope.currentUser = null;
    $scope.cardData = [];
    $scope.dashboardDataArray = [];
    $scope.categoriesList = [];
    $scope.prioritiesList = [];
    $scope.statusList = [];
    $scope.selectedIncident = null;

    $scope.currentAdminView = 'overview';
    $scope.allUsersList = [];
    $scope.userFilter = '';
    $scope.selectedUserEdit = null;
    $scope.reportStatusFilter = '';
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
                $scope.fetchAllGraphs();
            }
        }
    };

    $scope.switchAdminView = function (view) {
        $scope.currentAdminView = view;
        if (view === 'users') { $scope.fetchAllUsers(); }
    };

    $scope.fetchAllUsers = function () {
        IncidentReportSystemService.getAllUsersService().then(function (response) {
            if (response && response.data && !response.data.error) {
                $scope.allUsersList = response.data;
            }
        });
    };

    $scope.editAdminUser = function (user) {
        $scope.selectedUserEdit = angular.copy(user);
        $scope.selectedUserEdit.RoleID = $scope.selectedUserEdit.RoleID.toString();
    };

    $scope.closeUserEdit = function () { $scope.selectedUserEdit = null; };

    $scope.saveUserEdit = function () {
        var updateData = {
            UserID: $scope.selectedUserEdit.UserID,
            FirstName: $scope.selectedUserEdit.FirstName, LastName: $scope.selectedUserEdit.LastName,
            PhoneNumber: $scope.selectedUserEdit.PhoneNumber, Address: $scope.selectedUserEdit.Address,
            RoleID: parseInt($scope.selectedUserEdit.RoleID)
        };

        IncidentReportSystemService.adminUpdateUserService(updateData).then(function (response) {
            if (response.data === "Success") {
                Swal.fire({ title: "Updated!", text: "User information saved.", icon: "success" });
                $scope.fetchAllUsers(); $scope.fetchUserStats(); $scope.fetchAllGraphs();
                $scope.selectedUserEdit = null;
            } else { Swal.fire({ title: "Failed", text: response.data, icon: "error" }); }
        });
    };

    $scope.deleteAdminUser = function (user) {
        Swal.fire({
            title: "Are you sure?",
            text: "You are about to delete " + user.FirstName + " " + user.LastName + ". This cannot be undone.",
            icon: "warning", showCancelButton: true, confirmButtonColor: "#ef4444", confirmButtonText: "Yes, delete user"
        }).then(function (result) {
            if (result.isConfirmed) {
                IncidentReportSystemService.adminDeleteUserService({ UserID: user.UserID }).then(function (response) {
                    if (response.data === "Success") {
                        Swal.fire({ title: "Deleted!", text: "User has been removed.", icon: "success" });
                        $scope.fetchAllUsers(); $scope.fetchUserStats(); $scope.fetchAllGraphs();
                    } else { Swal.fire({ title: "Cannot Delete", text: response.data, icon: "error" }); }
                });
            }
        });
    };

    $scope.fetchUserStats = function () {
        IncidentReportSystemService.getUserStatsService().then(function (response) {
            if (response && response.data && !response.data.error) {
                $scope.userStats = response.data;
                if ($scope.userStats.Total > 0) {
                    $scope.pieLabels = ["Residents", "Officials"];
                    $scope.pieData = [$scope.userStats.Residents, $scope.userStats.Officials];
                    $scope.pieColors = ['#22c55e', '#facc15'];
                } else { $scope.pieData = []; }
            }
        });
    };

    $scope.fetchAllGraphs = function () {
        IncidentReportSystemService.getLineGraphService().then(function (response) {
            if (response && response.data && Array.isArray(response.data.labels)) {
                $scope.lineLabels = response.data.labels; $scope.lineSeries = response.data.series; $scope.lineData = response.data.data;

                $scope.lineColors = $scope.lineSeries.map(function (s) {
                    var lower = s.toLowerCase();
                    if (lower.includes('resolved')) return '#22c55e';
                    if (lower.includes('dismissed')) return '#ff4040';
                    if (lower.includes('pending')) return '#facc15';
                    if (lower.includes('progress')) return '#38bdf8';
                    return '#94a3b8';
                });
            } else { $scope.lineData = []; }
        }).catch(function () { $scope.lineData = []; });

        IncidentReportSystemService.getBarGraphService().then(function (response) {
            if (response && response.data && Array.isArray(response.data.labels)) {
                $scope.barLabels = response.data.labels; $scope.barSeries = response.data.series; $scope.barData = response.data.data;

                $scope.barColors = $scope.barSeries.map(function (c) {
                    var lower = c.toLowerCase();
                    if (lower.includes('water')) return '#38bdf8';
                    if (lower.includes('street') || lower.includes('light')) return '#facc15';
                    if (lower.includes('securit') || lower.includes('crime')) return '#ef4444';
                    if (lower.includes('noise')) return '#f97316';
                    if (lower.includes('garbage')) return '#8b5cf6';
                    if (lower.includes('road')) return '#64748b';
                    return '#94a3b8';
                });
            } else { $scope.barData = []; }
        }).catch(function () { $scope.barData = []; });
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

        IncidentReportSystemService.submitIncidentService(reportData).then(function (returnedData) {
            if (returnedData.data === "Success") {
                Swal.fire({ title: "Report Submitted!", text: "Your incident has been sent.", icon: "success" });
                $scope.clearReportFields(); $scope.showReportForm = false;
                $scope.fetchDashboardData();
                $scope.getIncidentCardStatus();
            } else { Swal.fire({ title: "Failed", text: returnedData.data, icon: "error" }); }
        });
    };

    $scope.viewIncident = function (data) {
        $scope.selectedIncident = angular.copy(data);
        $scope.selectedIncident.incident.StatusID = $scope.selectedIncident.incident.StatusID.toString();
        if ($scope.statusList.length === 0) {
            IncidentReportSystemService.getStatusListService().then(function (response) {
                if (response.data && !response.data.error) $scope.statusList = response.data;
            });
        }
    };

    $scope.closeIncidentView = function () { $scope.selectedIncident = null; };

    $scope.updateStatusFunc = function () {
        var updateData = { IncidentID: $scope.selectedIncident.incident.IncidentID, StatusID: parseInt($scope.selectedIncident.incident.StatusID) };
        IncidentReportSystemService.updateStatusService(updateData).then(function (response) {
            if (response.data === "Success") {
                Swal.fire({ title: "Updated!", text: "Status has been changed.", icon: "success" });
                $scope.fetchDashboardData();
                $scope.getIncidentCardStatus();
                if ($scope.currentUser && $scope.currentUser.RoleID === 3) { $scope.fetchAllGraphs(); } // Refresh Admin Charts
                $scope.selectedIncident = null;
            } else { Swal.fire({ title: "Failed", text: response.data, icon: "error" }); }
        });
    };

    $scope.fetchDashboardData = function () {
        IncidentReportSystemService.getDashboardDataService().then(function (response) {
            if (response && response.data && !response.data.error) {
                var formattedData = response.data.map(function (item) {
                    if (item.incident.CreatedAt && typeof item.incident.CreatedAt === "string" && item.incident.CreatedAt.indexOf("/Date") !== -1) {
                        item.incident.CreatedAt = parseInt(item.incident.CreatedAt.replace(/[^0-9]/g, ""));
                    } return item;
                });
                $scope.dashboardDataArray = formattedData;
            }
        });
    };

    $scope.getIncidentCardStatus = function () {
        IncidentReportSystemService.GetStatusCardService().then(function (returnedData) {
            if (returnedData && returnedData.data) { $scope.cardData = returnedData.data; }
        });
    };

    $scope.fetchDropdowns = function () {
        IncidentReportSystemService.getDropdownDataService().then(function (response) {
            if (response && response.data && !response.data.error) { $scope.categoriesList = response.data.categories; $scope.prioritiesList = response.data.priorities; }
        });
        IncidentReportSystemService.getStatusListService().then(function (response) {
            if (response && response.data && !response.data.error) { $scope.statusList = response.data; }
        });
    };

    $scope.loginFunc = function () {
        if (!$scope.loginUsername || !$scope.loginPassword) { Swal.fire({ title: "Missing Fields", icon: "warning" }); return; }
        var loginData = { Username: $scope.loginUsername, PasswordHash: $scope.loginPassword };

        IncidentReportSystemService.loginService(loginData).then(function (response) {
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

        IncidentReportSystemService.UpsertService(userData).then(function (returnedData) {
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

    $scope.clearFields = function () { $scope.FirstName = ""; $scope.MiddleName = ""; $scope.LastName = ""; $scope.Username = ""; $scope.Pname = ""; $scope.ConfirmPass = ""; $scope.Phonenumber = ""; $scope.Address = ""; $scope.Role = ""; $scope.submitted = false; };
    $scope.clearReportFields = function () { $scope.incidentTitle = ""; $scope.incidentLocation = ""; $scope.incidentDesc = ""; $scope.incidentCategory = ""; $scope.incidentPriority = ""; $scope.reportSubmitted = false; };
});