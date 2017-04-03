'use strict';

var gestionCandidatApp = angular.module('gestionCandidatApp', ['ngRoute']);



gestionCandidatApp.config(function ($routeProvider) {
    $routeProvider
    .when('/menu', {
        templateUrl: 'menu.html',
        controller: 'menuController'
    })
    .when('/recherche', {
        templateUrl: 'recherche.html',
        controller: 'rechercheController'
    })
    .when('/ajouter', {
        templateUrl: 'ajouter.html',
        controller: 'ajouterController'
    })
    .when('/agenda', {
        templateUrl: 'agenda.html',
        controller: 'agendaController'
    })
    .when('/message', {
        templateUrl: 'message.html',
        controller: 'messageController'
    })
});


gestionCandidatApp.controller("verifAuth", ['$scope', '$http', '$window', function ($scope, $http, $window) {
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
            $window.location.href = 'menu.html';
        }, (err) => {
            console.log(err);
        });

        

    }
}]);

gestionCandidatApp.controller("verifRecherche", ['$scope', '$http', '$window', function ($scope, $http, $window) {

    $scope.sendCandidat = function (candidat) {
        var req = {
            method: 'POST',
            url: 'http://localhost:55404/authentificationWebService.asmx/rechercheValue',
            responseType: "json",
            data: { nom: candidat.nom, prenom: candidat.prenom, telephone: candidat.telephone, email: candidat.email, token : 3232323232 }
        }

        $http(req).then(function (response) {
            $scope.email = response.data.d.email;
            $scope.password = response.data.d.password;
            $window.location.href = 'coordonnee.html';
        }, (err) => {
            console.log(err);
        });

    }
}]);