'use strict';

var gestionCandidatApp = angular.module('gestionCandidatApp', ['ngRoute', 'ngCookies']);

gestionCandidatApp.controller('app', function ($scope, $cookies) {
    $scope.myCookieVal = $cookies.get('cookie');
    $scope.setCookie = function (val) {
        $cookies.put('cookie', val);
    }
});



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


gestionCandidatApp.controller("verifAuth", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {
    $scope.sendEntry = function (utilisateur) {
        var req = {
            method: 'POST',
            url: 'http://192.168.1.31:5000/api/user/Candidates/sql/',
            responseType: "json",
            data: { email: utilisateur.email, password: utilisateur.password }
        }

        $http(req).then(function (response) {
            if (response.data.content != null) {
                console.log("In the error");
                $scope.errtxt = response.data.content;
            } else {
                console.log("En attente du cookie");
                $cookies.put('cookie', response.data.sessionId);
                $scope.sessionId = response.data.sessionId;
                $scope.name = response.data.name;
                $scope.email = response.data.email;
                $scope.password = response.data.password;
                $window.location.href = 'menu.html';
            }
            
            
        }, (err) => {
            console.log("ceci est une erreur" + err);
        });
    }
}]);


gestionCandidatApp.controller('rechercheJson', function ($scope, $http) {
    $http.get('json/responseRecherche.json')
         .then(function (response) {
             $scope.todos = response.data;
             if (response.data !== null) {
                 $scope.todos = response.data;
             } else {
                 console.log("In the error");
             }
         });
});


gestionCandidatApp.controller("verifRecherche", ['$scope', '$http', '$window', function ($scope, $http, $window) {

    $scope.sendCandidat = function (candidat) {
        var req = {
            method: 'POST',
            url: 'http://localhost:55404/authentificationWebService.asmx/rechercheValue',
            responseType: "json",
            data: { nom: candidat.nom, prenom: candidat.prenom, telephone: candidat.telephone, email: candidat.email}
        }

        $http(req).then(function (response) {
            if (response.data.content != null) {
                console.log("In the error");
                $scope.errtxt = response.data.content;
            } else {
                console.log("En attente du cookie");
                $scope.sessionId = response.data.sessionId;
                $scope.name = response.data.name;
                $scope.email = response.data.email;
                $scope.password = response.data.password;
                $scope.nom = response.data.nom;
                $scope.prenom = response.data.prenom;
                $scope.sexe = response.data.sexe;
                $scope.phone = response.data.phone;
                $scope.actions = response.data.actions;
                $scope.annee = response.data.annee;
                $scope.lien = response.data.lien;
                $scope.crCall = response.data.crCall;
                $scope.NS = response.data.NS;
                $scope.approche_email = response.data.approche_email;
                $scope.note = response.data.note;
                $scope.link = response.data.link;
                $scope.xpNote = response.data.xpNote;
                $scope.nsNote = response.data.nsNote;
                $scope.jobIdealNote = response.data.jobIdealNote;
                $scope.pisteNote = response.data.pisteNote;
                $scope.pieCouteNote = response.data.pieCouteNote;
                $scope.locationNote = response.data.locationNote;
                $scope.EnglishNote = response.data.EnglishNote;
                $scope.nationalityNote = response.data.nationalityNote;
                $scope.competences = response.data.competences;
                $window.location.href = 'coordonnee.html';
            }
        }, (err) => {
            console.log(err);
        });

    }
}]);