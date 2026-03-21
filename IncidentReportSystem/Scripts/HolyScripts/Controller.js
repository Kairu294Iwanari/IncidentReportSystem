app.controller("IncidentReportSystemController", function ($scope, IncidentReportSystemService) {

    $scope.showLogin = false;
    $scope.currentUser = null;
    $scope.userArray = [];
    $scope.reportArray = [];

    $scope.initialRun = function () {
        $scope.sessionParsing();
        $scope.checkRegistration();
        $scope.currentUser = IncidentReportSystemService.getCurrentUser();
    };

    $scope.sessionParsing = function () {
        var users = sessionStorage.getItem("UserArray");
        $scope.userArray = users ? JSON.parse(users) : [];
    };

    $scope.saveSession = function () {
        sessionStorage.setItem("UserArray", JSON.stringify($scope.userArray));
    };

    $scope.checkRegistration = function () {
        $scope.showLogin = $scope.userArray.length >= 1;
    };

    $scope.addFunc = function () {
        if (!$scope.Fname || !$scope.Lname || !$scope.Uname ||
            !$scope.Pname || !$scope.ConfirmPass || !$scope.Role ||
            !$scope.Phone || !$scope.Address) {
            Swal.fire({ title: "Fields not completed", text: "Please fill in all required fields.", icon: "warning" });
            return;
        }

        if ($scope.Pname.length < 8 || $scope.Pname.length > 16) {
            Swal.fire({ title: "Weak Password", text: "Password must be between 8 and 16 characters.", icon: "warning" });
            return;
        }

        if ($scope.Pname !== $scope.ConfirmPass) {
            Swal.fire({ title: "Mismatch", text: "Passwords do not match.", icon: "warning" });
            return;
        }

        if (!$scope.Phone || $scope.Phone.length !== 10) {
            Swal.fire({ title: "Invalid Phone", text: "Phone number requires exactly 10 digits.", icon: "warning" });
            return;
        }

        var existing = $scope.userArray.find(function (item) {
            return item.Username.toLowerCase() === $scope.Uname.toLowerCase();
        });

        if (existing) {
            Swal.fire({ title: "Username Taken", text: "Please choose another username.", icon: "error" });
            return;
        }

        var name = $scope.Fname;

        $scope.userArray.push({
            FirstName: $scope.Fname,
            MiddleName: $scope.Mname || "",
            LastName: $scope.Lname,
            Username: $scope.Uname,
            Password: $scope.Pname,
            Phone: $scope.Phone,
            Address: $scope.Address,
            Role: $scope.Role
        });

        $scope.saveSession();
        $scope.checkRegistration();
        $scope.clearFields();

        Swal.fire({ title: "Account Created!", text: name + " has been successfully registered.", icon: "success" });
    };

    $scope.delFunc = function () {
        Swal.fire({
            title: "Are you sure?",
            text: "Clear all fields?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            confirmButtonText: "Yes"
        }).then(function (result) {
            if (result.isConfirmed) {
                if (!$scope.$$phase) {
                    $scope.$apply(function () { $scope.clearFields(); });
                } else {
                    $scope.clearFields();
                }
                Swal.fire({ title: "Cleared.", text: "Fields have been cleared.", icon: "success" });
            }
        });
    };

    $scope.clearFields = function () {
        $scope.Fname = "";
        $scope.Mname = "";
        $scope.Lname = "";
        $scope.Uname = "";
        $scope.Pname = "";
        $scope.ConfirmPass = "";
        $scope.Phone = "";
        $scope.Address = "";
        $scope.Role = "";
    };

    $scope.checkLogin = function () {
        if (!$scope.loginUsername || !$scope.loginPassword) {
            Swal.fire({ title: "Fields not filled.", text: "Enter your username and password.", icon: "warning" });
            return;
        }

        var found = $scope.userArray.find(function (item) {
            return item.Username === $scope.loginUsername &&
                item.Password === $scope.loginPassword;
        });

        if (found) {
            IncidentReportSystemService.saveCurrentUser(found);
            Swal.fire({ title: "Login Successful!", text: "Welcome, " + found.FirstName + "!", icon: "success", confirmButtonColor: "#2e7d32" })
                .then(function (result) {
                    if (result.isConfirmed) {
                        if (found.Role === "Resident") window.location.href = "/Incident/ResidentDashboard";
                        if (found.Role === "Barangay Official") window.location.href = "/Incident/OfficialDashboard";
                        if (found.Role === "Admin") window.location.href = "/Incident/AdminDashboard";
                    }
                });
        } else {
            Swal.fire({ title: "Login Failed.", text: "Invalid Username or Password.", icon: "error", confirmButtonColor: "#c62828" });
        }
    };

    $scope.logoutFunc = function () {
        Swal.fire({
            title: "Are you sure?",
            text: "You will be logged out.",
            icon: "question",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            confirmButtonText: "Yes, logout"
        }).then(function (result) {
            if (result.isConfirmed) {
                IncidentReportSystemService.clearCurrentUser();
                window.location.href = "/Incident/LoginPage";
            }
        });
    };

});