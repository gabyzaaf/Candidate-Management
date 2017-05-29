'use strict';

var gestionCandidatApp = angular.module('gestionCandidatApp', ['ngRoute', 'ngCookies']);


/********************   Session ID   ********************/

gestionCandidatApp.controller('app', function ($scope, $cookies) {
    $scope.myCookieVal = $cookies.get('cookie');
    $scope.setCookie = function (val) {
        $cookies.put('cookie', val);
    }
});

/********************************************************/

/********************   Authentification   ********************/

gestionCandidatApp.controller("verifAuth", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {
    $scope.sendEntry = function (utilisateur) {
        var req = {
            method: 'POST',
            url: 'http://192.168.0.14:5000/api/user/admin/auth/',
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

/****************************************************************/

/*********************  Ajouter un candidat  ********************/

gestionCandidatApp.controller("addCandidate", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {
    $scope.sendEntry = function (candidat) {
        var req = {
            method: 'POST',
            url: 'http://192.168.0.14:5000/api/user/add/candidat/',
            responseType: "json",
            data: {
                session_id: $cookies.get('cookie'),
                Name: candidat.Name,
                Firstname: candidat.Firstname,
                emailAdress: candidat.emailAdress,
                phone: candidat.phone,
                sexe: "M",
                actions: candidat.actions,
                year: candidat.year,
                link: candidat.link,
                crCall: candidat.crCall,
                ns: candidat.ns
            }
        }

        $http(req).then(function (response) {
            $scope.contentResponse = response.data.content;
            if (response.data.content != "Le candidat a ete ajoute à votre systeme") {
                console.log("In the error");
                
            } else {
                console.log("En attente du cookie baby");
                console.log(response.data.content);
            }


        }, (err) => {
            console.log("ceci est une erreur" + err);
        });
    }
}]);

/****************************************************************/

/*********************  Ajouter un entrtien  ********************/

gestionCandidatApp.controller("addCandidate", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {
    $scope.sendEntry = function (candidat) {
        var req = {
            method: 'POST',
            url: 'http://192.168.0.14:5000/api/user/add/candidat/',
            responseType: "json",
            data: {
                session_id: $cookies.get('cookie'),
                Name: candidat.Name,
                Firstname: candidat.Firstname,
                emailAdress: candidat.emailAdress,
                phone: candidat.phone,
                sexe: "M",
                actions: candidat.actions,
                year: candidat.year,
                link: candidat.link,
                crCall: candidat.crCall,
                ns: candidat.ns
            }
        }

        $http(req).then(function (response) {
            $scope.contentResponse = response.data.content;
            if (response.data.content != "Le candidat a ete ajoute à votre systeme") {
                console.log("In the error");

            } else {
                console.log("En attente du cookie baby");
                console.log(response.data.content);
            }


        }, (err) => {
            console.log("ceci est une erreur" + err);
        });
    }
}]);

/****************************************************************/

/*********************  Modifier un candidat  ********************/

gestionCandidatApp.controller("updateCandidate", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {

    $scope.sendEntry = function (updateCandidate) {
        if ($scope.updateCandidate.nom == null) {
            $scope.updateCandidate.nom = $scope.nomCoor;
        }
        if ($scope.updateCandidate.prenom == null) {
            $scope.updateCandidate.prenom = $scope.prenomCoor;
        }
        if ($scope.updateCandidate.email == null) {
            $scope.updateCandidate.email = $scope.emailCoor;
        }
        if ($scope.updateCandidate.telephone == null) {
            $scope.updateCandidate.telephone = $scope.phoneCoor;
        }
        if ($scope.updateCandidate.sexe == null) {
            $scope.updateCandidate.sexe = $scope.sexeCoor;
        }
        if ($scope.updateCandidate.actions == null) {
            $scope.updateCandidate.actions = $scope.actionsCoor;
        }
        if ($scope.updateCandidate.anneediplome == null) {
            $scope.updateCandidate.anneediplome = $scope.anneeCoor;
        }
        if ($scope.updateCandidate.url == null) {
            $scope.updateCandidate.url = $scope.lienCoor;
        }
        if ($scope.updateCandidate.cr == null) {
            $scope.updateCandidate.cr = $scope.crCallCoor;
        }
        if ($scope.updateCandidate.note == null) {
            $scope.updateCandidate.note = $scope.NSCoor;
        }
        var req = {
            method: 'POST',
            url: 'http://192.168.0.14:5000/api/user/update/candidat/',
            responseType: "json",
            data: {
                session_id: $cookies.get('cookie'),
                Name: updateCandidate.nom,
                Firstname: updateCandidate.prenom,
                emailAdress: updateCandidate.email,
                phone: updateCandidate.telephone,
                sexe: updateCandidate.sexe,
                actions: updateCandidate.actions,
                year: updateCandidate.anneediplome,
                link: updateCandidate.url,
                crCall: updateCandidate.cr,
                ns: updateCandidate.note
            }
        }

        $http(req).then(function (response) {
            $scope.contentResponse = response.data.content;
            if (response.data.content != "Le candidat a ete modifie dans votre systeme") {
                console.log("In the error");
                console.log(updateCandidate.nom);
                console.log(updateCandidate.prenom);
                console.log(updateCandidate.email);
                console.log(updateCandidate.telephone);
                console.log(updateCandidate.sexe);
                console.log(response.data.content);
            } else {
                console.log("En attente du cookie");
                console.log(response.data.content);
            }


        }, (err) => {
            console.log("ceci est une erreur" + err);
        });
    }
}]);

/****************************************************************/

/*********************  Ajouter un message  ********************/

gestionCandidatApp.controller("addMessage", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {
    $scope.sendEntry = function (message) {
        var req = {
            method: 'POST',
            url: 'http://192.168.0.14:5000/api/user/add/message/',
            responseType: "json",
            data: {
                session_id: $cookies.get('cookie'),
                titre: message.titre,
                contenu: message.contenu,
            }
        }

        $http(req).then(function (response) {
            $scope.contentResponse = response.data.content;
            if (response.data.content != "Le message a ete ajoute à votre systeme") {
                console.log("In the error");

            } else {
                console.log("En attente du cookie baby");
                console.log(response.data.content);
            }


        }, (err) => {
            console.log("ceci est une erreur" + err);
        });
    }
}]);

/****************************************************************/

/*********************  modifier un message  ********************/

gestionCandidatApp.controller("updateMessage", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {
    $scope.sendEntry = function (updateMessage) {
        if ($scope.updateMessage.titre == null) {
            $scope.updateMessage.titre = $scope.titreMsgUpdate;
        }
        if ($scope.updateMessage.contenu == null) {
            $scope.updateMessage.contenu = $scope.contenuMsgUpdate;
        }
        var req = {
            method: 'POST',
            url: 'http://192.168.0.14:5000/api/user/update/message/',
            responseType: "json",
            data: {
                session_id: $cookies.get('cookie'),
                titre: updateMessage.titre,
                contenu: updateMessage.contenu,
            }
        }

        $http(req).then(function (response) {
            $scope.contentResponse = response.data.content;
            if (response.data.content != "Le message a ete ajoute à votre systeme") {
                console.log("In the error");
                console.log(response.data.content);
                console.log($scope.titre);
                console.log($scope.titreMsgUpdate);
            } else {
                console.log("En attente du cookie baby");
                console.log(response.data.content);
                console.log($scope.titre);
                console.log($scope.titreMsgUpdate);
            }


        }, (err) => {
            console.log("ceci est une erreur" + err);
        });
    }
}]);

/****************************************************************/

/*********************  Recherche un candidat  ********************/

gestionCandidatApp.controller("rechercheCandidat", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {

    $scope.sendCandidat = function (candidat) {
        $http.get('http://192.168.0.14:5000/api/user/Candidates/recherche/' + candidat.nom + '/' + $cookies.get('cookie')).then(function (response) {
            $scope.todos = response.data;
            $scope.selectedCar = $cookies.get('email');
            if ($scope.todos != null) {
                if ($scope.todos[0].content != null) {
                    $scope.contentResponse = $scope.todos[0].content;
                    console.log($scope.todos[0].content);

                } else {
                    var nbSelect = 0;
                    for (var i = 0; i < $scope.todos.length; i++) {
                        if ($scope.todos[i].phone == $scope.selectedCar) {
                            nbSelect = i;
                            console.log($scope.todos[i].phone);
                        }
                        console.log($scope.todos[i].phone);
                    }
                    $scope.nomCoor = $scope.todos[nbSelect].nom;
                    $scope.prenomCoor = $scope.todos[nbSelect].prenom;
                    $scope.phoneCoor = $scope.todos[nbSelect].phone;
                    $scope.emailCoor = $scope.todos[nbSelect].email;
                    $scope.sexeCoor = $scope.todos[nbSelect].sexe;
                    console.log($scope.todos[nbSelect].sexe);
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
            } else {
                console.log("In the error");
            }
        }, (err) => {
            console.log(err);
        });

    }
}]);

/*************************************************************************/

/*********************  Recherche tous les candidats  ********************/

gestionCandidatApp.controller("rechercheAllCandidat", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {

        $http.get('http://192.168.0.14:5000/api/user/Candidates/rechercheAllCandidate').then(function (response) {
            $scope.todos = response.data;
            $scope.selectedCar = $cookies.get('id');
            if ($scope.todos != null) {
                if ($scope.todos[0].content != null) {
                    $scope.contentResponse = $scope.todos[0].content;
                    console.log($scope.todos[0].content);

                } else {
                    var nbSelect = 0;
                    for (var i = 0; i < $scope.todos.length; i++) {
                        if ($scope.todos[i].phone == $scope.selectedCar) {
                            nbSelect = i;
                        }
                    }
                    $scope.id = $scope.todos[nbSelect].id;
                    $scope.nomCoor = $scope.todos[nbSelect].nom;
                    $scope.prenomCoor = $scope.todos[nbSelect].prenom;
                    $scope.phoneCoor = $scope.todos[nbSelect].phone;
                    $scope.emailCoor = $scope.todos[nbSelect].email;
                }
            } else {
                console.log("In the error");
            }
        }, (err) => {
            console.log(err);
        });
}]);

/*************************************************************************/

/*********************  Recherche tous les messages  ********************/

gestionCandidatApp.controller("rechercheAllMessage", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {

    $http.get('http://192.168.0.14:5000/api/user/searchAllMessage/message/').then(function (response) {
        $scope.todos = response.data;

        if ($scope.todos != null) {
            if ($scope.todos[0].content != null) {
                $scope.contentResponse = $scope.todos[0].content;
                console.log($scope.todos[0].content);

            } else {
                var nbSelect = 0;
                for (var i = 0; i < $scope.todos.length; i++) {
                    if ($scope.todos[i].titre == $scope.selectedMsg) {
                        nbSelect = i;
                    }
                }
                $scope.titreMsgUpdate = $scope.todos[nbSelect].titre;
                $scope.contenueMsgUpdate = $scope.todos[nbSelect].contenu;
            }
        } else {
            console.log("In the error");
        }
    }, (err) => {
        console.log(err);
    });
}]);

/*************************************************************************/

/*
gestionCandidatApp.controller("rechercheCandidat", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {

    var req = {
        method: 'GET',
        url: 'http://192.168.0.14:5000/api/user/Candidates/recherche/',
        responseType: "json",
    }

    $http(req).then(function (response) {
        console.log(response);
        $scope.contentResponse = response.data.content;
        if (response.data.content != "Le message a ete ajoute à votre systeme") {
            console.log("In the error");

        } else {
            console.log("En attente du cookie baby");
            console.log(response.data.content);
        }
    });
}]);
*/
/******************************************************************/

/*********************  Test afficher template message avec fichier json  ********************/
/*
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
*/
/******************************************************************************************/

/*********************  Test recherche un candidat avec fichier json  ********************/
/*
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
*/
/**************************************************************************************/

/*
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
*/

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