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

gestionCandidatApp.controller('rechercheJsonMessage', function ($scope, $http, $cookies, $window) {
    $http.get('json/responseMessage.json')
         .then(function (response) {
             //$scope.selectedCar = $cookies.get('emailCandidat');
             $scope.todos = response.data;
             if (response.data != null) {
                 $scope.todos = response.data;
             } else {
                 console.log("In the error");
             }
         });
    $scope.sendMessage = function () {
        console.log("alloMessage");
        var nbSelect = 0;
        for (var i = 0; i < $scope.todos.length; i++) {
            if ($scope.todos[i].id == $scope.selectMessage) {
                nbSelect = i;
            }
        }
        $scope.idMsg = $scope.todos[nbSelect].id;
        $scope.titreMsg = $scope.todos[nbSelect].titre;
        $scope.contenuMsg = $scope.todos[nbSelect].contenu;
    }
});

gestionCandidatApp.controller('rechercheJson', function ($scope, $http, $cookies, $window) {
    $http.get('json/responseRecherche.json')
         .then(function (response) {
             $scope.selectedCar = $cookies.get('emailCandidat');
             $scope.todos = response.data;
             if (response.data != null) {
                 $scope.todos = response.data;
             } else {
                 console.log("In the error");
             }
         });
    $scope.sendCoordonnee = function () {
        console.log("alloRecherche");
        var nbSelect = 0;
        for (var i = 0; i < $scope.todos.length; i++) {
            if ($scope.todos[i].email == $scope.selectedCar) {
                nbSelect = i;
            }
        }
        $scope.nomCoor = $scope.todos[nbSelect].nom;
        $scope.prenomCoor = $scope.todos[nbSelect].prenom;
        $scope.phoneCoor = $scope.todos[nbSelect].phone;
        $scope.emailCoor = $scope.todos[nbSelect].email;
        $scope.cpCoor = $scope.todos[nbSelect].cp;
        $scope.actionsCoor = $scope.todos[nbSelect].actions;
        $scope.anneeCoor = $scope.todos[nbSelect].annee;
        $scope.lienCoor = $scope.todos[nbSelect].lien;
        $scope.crCallCoor = $scope.todos[nbSelect].crCall;
        $scope.NSCoor = $scope.todos[nbSelect].NS;
        $scope.approche_emailCoor = $scope.todos[nbSelect].approche_email;
        $scope.noteCoor = $scope.todos[nbSelect].note;
        $scope.linkCoor = $scope.todos[nbSelect].link;
        $scope.xpNoteCoor = $scope.todos[nbSelect].xpNote;
        $scope.nsNoteCoor = $scope.todos[nbSelect].nsNote;
        $scope.jobIdealNoteCoor = $scope.todos[nbSelect].jobIdealNote;
        $scope.pisteNoteCoor = $scope.todos[nbSelect].pisteNote;
        $scope.pieCouteNoteCoor = $scope.todos[nbSelect].pieCouteNote;
        $scope.locationNoteCoor = $scope.todos[nbSelect].locationNote;
        $scope.EnglishNoteCoor = $scope.todos[nbSelect].EnglishNote;
        $scope.nationalityNoteCoor = $scope.todos[nbSelect].nationalityNote;
        $scope.competencesCoor = $scope.todos[nbSelect].competences;
      
    }
});

gestionCandidatApp.controller("verifRecherche", ['$scope', '$http', '$window', function ($scope, $http, $window) {

    $scope.sendCandidat = function (candidat) {
        var req = {
            method: 'POST',
            url: 'http://localhost:55404/authentificationWebService.asmx/rechercheValue',
            responseType: "json",
            data: { nom: candidat.nom, prenom: candidat.prenom, telephone: candidat.telephone, email: candidat.email }
        }

        $http(req).then(function (response) {
            if (response.data.content != null) {
                console.log("In the error");
                $scope.errtxt = response.data.content;
            } else {

                console.log("En attente du cookie");
                if (candidat.email == $scope.selectedCar) {
                    console.log("c'est ok")
                    console.log($scope.selectedCar)
                    console.log($scope.todos.length)
                } else {
                    console.log($scope.selectedCar)
                    console.log("c'est pas ok")
                }
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
            }
        }, (err) => {
            console.log(err);
        });

    }
}]);

/*
gestionCandidatApp.controller("afficherCoordonnee", ['$scope', '$http', '$window', $cookies, function ($scope, $http, $window, $cookies) {
    
    for (var i = 0; $scope.todos.length < i; i++)
    {
        if ($scope.todos[i].email == $scope.selectedCar)
        {
            $scope.nomCoor = $scope.todos[i].nom;
            console.log(i)
            console.log($scope.todos[i].nom)
        }
        
    }
    console.log("En attente du cookie");
    
    if (candidat.nom == $scope.selectedCar) {
        console.log("c'est ok")
        console.log($scope.selectedCar)
    } else {
        console.log($scope.selectedCar)
        console.log("c'est pas ok")
    }
    //$window.location.href = 'coordonnee.html';
}]);*/