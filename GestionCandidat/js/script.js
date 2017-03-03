'use strict';

var authentificationApp = angular.module('authentificationApp', []);

authentificationApp.controller("headerCtrl", function ($scope) {
    $scope.message = "Authentification";
});

authentificationApp.controller("footerCtrl", function ($scope) {
    $scope.message = "OK";
});

authentificationApp.controller("contentCtrl", ['$scope', '$http', function ($scope, $http) {
    $scope.sendEntry = function (utilisateur) {
        var req = {
            method: 'POST',
            url: 'http://localhost:55404/authentificationWebService.asmx/receptionValue',
            responseType: "json",
            data: { email: utilisateur.email, password: utilisateur.password }
        }

        $http(req).then(function (response) {
            $scope.email = response.data.d.email;
            $scope.password = response.data.d.password;
        });

    }
}]);