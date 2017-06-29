'use strict';

var gestionCandidatApp = angular.module('gestionCandidatApp', ['ngRoute', 'ngCookies']);


/********************   machine learning azure   ********************/

gestionCandidatApp.controller('predictionAzure', ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {
    $scope.sendValueForAzure = function (prediction) {
        var req = {
            method: 'POST',
            url: 'https://ussouthcentral.services.azureml.net/workspaces/f976db5dc6194a59a479f37fc96dffd4/services/ce01fbb1b995484582eaaa8639bd3884/execute?api-version=2.0&details=true',
            responseType: "json",
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer lvoOpIc0yr72Hozn3/+MLJtCuH8Ki8se1pxwNESj/CK/H0RLqDoI8SblrgWQ0e5wHLB+1dRliLPMwH2n3ktKYA==',
                'Access-Control-Allow-Origin': '*'
            },
            data: {
                "Inputs": {
                    "input1": {
                        "ColumnNames": [
                          "satisfaction_level",
                          "last_evaluation",
                          "number_project",
                          "average_montly_hours",
                          "time_spend_company",
                          "Work_accident",
                          "promotion_last_5years",
                          "sales",
                          "salary"
                        ],
                        "Values": [
                          [
                            "0.53",
                            "0.68",
                            "4",
                            "160",
                            "0",
                            "0",
                            "0",
                            "sales",
                            "low"
                          ]
                        ]
                    }
                },
                "GlobalParameters": {}
            },

        }

        $http(req).then(function (response) {
            console.log(response.data);
        }, (err) => {
            console.log("ceci est une erreur" + err);
        });
    }
}]);

/*******************************************************************/

/********************   Analyse Candidat   ********************/

gestionCandidatApp.controller('GraphCtrl', ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {
    $http.get('http://192.168.0.16:5000/api/user/MlCandidates/recherche/' + $cookies.get('cookie')).then(function (response) {
        $scope.todos = response.data;
        for (var i = 0; i < 500; i++) {
            if ($scope.todos[0].content != null) {
                console.log($scope.todos[0].content);
            } else {
                if (i == 0) {
                    $scope.satisfaction_level = $scope.todos[i].satisfaction_level.replace(",", ".");;
                    $scope.last_evaluation = $scope.todos[i].last_evaluation.replace(",", ".");;
                    $scope.c = $scope.todos[i].number_project;
                    $scope.average_montly_hours = $scope.todos[i].average_montly_hours;
                    $scope.time_spend_company = $scope.todos[i].time_spend_company;
                    $scope.Work_accident = $scope.todos[i].Work_accident;
                    $scope.left_work = $scope.todos[i].left_work;
                    $scope.promotion_last_5years = $scope.todos[i].promotion_last_5years;
                    $scope.sales = $scope.todos[i].sales;
                    $scope.salary = $scope.todos[i].salary;
                } else {
                    $scope.satisfaction_level = $scope.satisfaction_level + ';' + $scope.todos[i].satisfaction_level.replace(",", ".");;
                    $scope.last_evaluation = $scope.last_evaluation + ';' + $scope.todos[i].last_evaluation.replace(",", ".");;
                    $scope.number_project = $scope.number_project + ';' + $scope.todos[i].number_project;
                    $scope.average_montly_hours = $scope.average_montly_hours + ';' + $scope.todos[i].average_montly_hours;
                    $scope.time_spend_company = $scope.time_spend_company + ';' + $scope.todos[i].time_spend_company;
                    $scope.Work_accident = $scope.Work_accident + ';' + $scope.todos[i].Work_accident;
                    $scope.left_work = $scope.left_work + ';' + $scope.todos[i].left_work;
                    $scope.promotion_last_5years = $scope.promotion_last_5years + ';' + $scope.todos[i].promotion_last_5years;
                    $scope.sales = $scope.sales + ';' + $scope.todos[i].sales;
                    $scope.salary = $scope.salary + ';' + $scope.todos[i].salary;
                }
            }
        };


        $scope.chart = null;
        $scope.chart2 = null;
        $scope.chart3 = null;
        $scope.chart4 = null;

        $scope.config = {};
        

        $scope.typeOptions = ["line", "bar", "spline", "step", "area", "area-step", "area-spline"];

        $scope.config.type1 = $scope.typeOptions[0];
        $scope.config.type2 = $scope.typeOptions[1];

        $scope.showGraph = function () {
            $scope.graph1 = "Graph 1 : Graphique sur les promotions et les nombres de projet";
            $scope.config["test1"] = $scope.number_project;
            $scope.config.data2 = $scope.promotion_last_5years;
            $scope.config.type1 = $scope.typeOptions[6];
            $scope.config.type2 = $scope.typeOptions[2];
            var config = {};
            config.bindto = '#chart';
            config.zoom = { "enabled": "true" };
            config.data = {};
            config.data.json = {};
            config.data.json.data1 = $scope.config["test1"].split(";");
            config.data.json.data2 = $scope.config.data2.split(";");
            config.axis = { "y": { "label": { "text": "Number of items", "position": "outer-middle" } } };
            config.data.types = { "test1": $scope.config.type1, "data2": $scope.config.type2 };
            $scope.chart = c3.generate(config);
        }

        $scope.showGraph2 = function () {
            $scope.graph2 = "Graph 2 : Graphique sur les niveaux de satisfaction du salarié et des promotions eux durant les 5 dernières années";
            $scope.config.data1 = $scope.satisfaction_level;
            $scope.config.data2 = $scope.promotion_last_5years;
            $scope.config.type1 = $scope.typeOptions[6];
            $scope.config.type2 = $scope.typeOptions[2];
            var config = {};
            config.bindto = '#chart2';
            config.zoom = { "enabled": "true" };
            config.data = {};
            config.data.json = {};
            config.data.json.data1 = $scope.config.data1.split(";");
            config.data.json.data2 = $scope.config.data2.split(";");
            config.axis = { "y": { "label": { "text": "Number of items", "position": "outer-middle" } } };
            config.data.types = { "data1": $scope.config.type1, "data2": $scope.config.type2 };
            $scope.chart2 = c3.generate(config);
        }

        $scope.showGraph3 = function () {
            $scope.graph3 = "Graph 3 : Graphique sur le nombre de projet et l'évaluation du salarié";
            $scope.config.data1 = $scope.number_project;
            $scope.config.data2 = $scope.last_evaluation;
            $scope.config.type1 = $scope.typeOptions[6];
            $scope.config.type2 = $scope.typeOptions[2];
            var config = {};
            config.bindto = '#chart3';
            config.zoom = { "enabled": "true" };
            config.data = {};
            config.data.json = {};
            config.data.json.data1 = $scope.config.data1.split(";");
            config.data.json.data2 = $scope.config.data2.split(";");
            config.axis = { "y": { "label": { "text": "Number of items", "position": "outer-middle" } } };
            config.data.types = { "data1": $scope.config.type1, "data2": $scope.config.type2 };
            $scope.chart3 = c3.generate(config);
        }

        $scope.showGraph4 = function () {
            $scope.graph4 = "Graph 4 : Graphique sur la satisfaction du salarié et le nombre de projet qu'il a effectué";
            $scope.config.data1 = $scope.satisfaction_level;
            $scope.config.data2 = $scope.number_project;
            $scope.config.type1 = $scope.typeOptions[6];
            $scope.config.type2 = $scope.typeOptions[2];
            var config = {};
            config.bindto = '#chart4';
            config.zoom = { "enabled": "true" };
            config.data = {};
            config.data.json = {};
            config.data.json.data1 = $scope.config.data1.split(";");
            config.data.json.data2 = $scope.config.data2.split(";");
            config.axis = { "y": { "label": { "text": "Number of items", "position": "outer-middle" } } };
            config.data.types = { "data1": $scope.config.type1, "data2": $scope.config.type2 };
            $scope.chart4 = c3.generate(config);
        }

    });
}]);

/********************************************************/

/********************   Se déconnecter   ********************/

gestionCandidatApp.controller('seDeconncter', ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {
    $scope.sendConnexion = function () {
        $cookies.put('cookie', "null");
        if ($cookies.get('cookie') == "null") {
            $window.location.href = 'index.html';
        }
    }

}]);

/********************************************************/

/********************   Vérification ID  ********************/

gestionCandidatApp.controller('verifId', ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {

        if ($cookies.get('cookie') == "null") {
            $window.location.href = 'index.html';
        }

}]);

/*************************************************************/


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
            url: 'http://192.168.0.16:5000/api/user/admin/auth/',
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
            url: 'http://192.168.0.16:5000/api/user/add/candidat/',
            responseType: "json",
            data: {
                session_id: $cookies.get('cookie'),
                Name: candidat.Name,
                Firstname: candidat.Firstname,
                emailAdress: candidat.emailAdress,
                phone: candidat.phone,
                sexe: candidat.sexe,
                action: candidat.action,
                year: candidat.year,
                link: candidat.link,
                crCall: candidat.crCall,
                ns: candidat.note,
                email: "true",
                cp : candidat.cp
            }
        }

        $http(req).then(function (response) {
            $scope.contentResponse = response.data.content;
            if (response.data.content != "Le candidat a ete ajoute à votre systeme") {
                console.log("In the error");
                console.log(candidat.action);
                console.log(candidat.Name);
                
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

/*********************  Ajouter un entretien  ********************/

gestionCandidatApp.controller("addEntretien", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {
    $scope.sendEntry = function (entretien) {
        console.log($scope.selectedCar);
        var req = {
            method: 'POST',
            url: 'http://192.168.0.16:5000/api/user/add/candidat/report',
            responseType: "json",
            data: {
                sessionId: $cookies.get('cookie'),
                emailCandidat : $scope.selectedCar,
                note: entretien.note,
                link: entretien.link,
                xpNote: entretien.xpNote,
                nsNote: entretien.nsNote,
                jobIdealNote: entretien.jobIdealNote,
                pisteNote: entretien.pisteNote,
                pieCouteNote: entretien.pieCouteNote,
                locationNote: entretien.locationNote,
                EnglishNote: entretien.EnglishNote,
                nationalityNote: entretien.nationalityNote,
                competences: entretien.competences
            }
        }

        $http(req).then(function (response) {
            $scope.contentResponse = response.data.content;
            if (response.data.content != "Le report a ete ajoute parfaitement à votre system") {
                console.log("In the error");

            } else {
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
        if ($scope.updateCandidate.action == null) {
            $scope.updateCandidate.action = $scope.actionsCoor;
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
        if ($scope.updateCandidate.cp == null) {
            $scope.updateCandidate.cp = $scope.cpCoor;
        }

        var req = {
            method: 'POST',
            url: 'http://192.168.0.16:5000/api/user/update/candidat/',
            responseType: "json",
            data: {
                session_id: $cookies.get('cookie'),
                Name: updateCandidate.nom,
                Firstname: updateCandidate.prenom,
                emailAdress: updateCandidate.email,
                cp: updateCandidate.cp,
                phone: updateCandidate.telephone,
                sexe: updateCandidate.sexe,
                action: updateCandidate.action,
                year: updateCandidate.anneediplome,
                link: updateCandidate.url,
                crCall: updateCandidate.cr,
                NS: updateCandidate.note,
                email: "true"
            }
        }

        $http(req).then(function (response) {
            $scope.contentResponse = response.data.content;
            if (response.data.content != "Le candidat a ete modifie dans votre systeme") {
                console.log("In the error");
                console.log(response.data.content);
            } else {
                console.log("En attente du cookie");
                console.log(updateCandidate.nom);
                console.log(updateCandidate.prenom);
                console.log(updateCandidate.email);
                console.log(updateCandidate.cp);
                console.log(updateCandidate.telephone);
                console.log(updateCandidate.sexe);
                console.log(updateCandidate.action);
                console.log(updateCandidate.anneediplome);
                console.log(updateCandidate.url);
                console.log(updateCandidate.cr);
                console.log(updateCandidate.note);
                console.log(response.data.content);
            }


        }, (err) => {
            console.log("ceci est une erreur" + err);
        });
    }
}]);

/****************************************************************/

/*********************  Modifier un entretien  ********************/

gestionCandidatApp.controller("updateEntretien", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {

    $scope.sendEntry = function (updateEntretien) {
        if ($scope.updateEntretien.link == null) {
            $scope.updateEntretien.link = $scope.linkCoor;
        }
        if ($scope.updateEntretien.note == null) {
            $scope.updateEntretien.note = $scope.noteCoor;
        }
        if ($scope.updateEntretien.nsNote == null) {
            $scope.updateEntretien.nsNote = $scope.nsNoteCoor;
        }
        if ($scope.updateEntretien.xpNote == null) {
            $scope.updateEntretien.xpNote = $scope.xpNoteCoor;
        }
        if ($scope.updateEntretien.jobIdealNote == null) {
            $scope.updateEntretien.jobIdealNote = $scope.jobIdealNoteCoor;
        }
        if ($scope.updateEntretien.pisteNote == null) {
            $scope.updateEntretien.pisteNote = $scope.pisteNoteCoor;
        }
        if ($scope.updateEntretien.pieCouteNote == null) {
            $scope.updateEntretien.pieCouteNote = $scope.pieCouteNoteCoor;
        }
        if ($scope.updateEntretien.locationNote == null) {
            $scope.updateEntretien.locationNote = $scope.locationNoteCoor;
        }
        if ($scope.updateEntretien.EnglishNote == null) {
            $scope.updateEntretien.EnglishNote = $scope.EnglishNoteCoor;
        }
        if ($scope.updateEntretien.nationalityNote == null) {
            $scope.updateEntretien.nationalityNote = $scope.nationalityNoteCoor;
        }
        if ($scope.updateEntretien.competences == null) {
            $scope.updateEntretien.competences = $scope.competencesCoor;
        }

        var req = {
            method: 'POST',
            url: 'http://192.168.0.16:5000/api/user/update/candidat/report',
            responseType: "json",
            data: {
                sessionId: $cookies.get('cookie'),
                emailCandidat: $scope.selectedCar,
                note: updateEntretien.note,
                link: updateEntretien.link,
                xpNote: updateEntretien.xpNote,
                nsNote: updateEntretien.nsNote,
                jobIdealNote: updateEntretien.jobIdealNote,
                pisteNote: updateEntretien.pisteNote,
                pieCouteNote: updateEntretien.pieCouteNote,
                locationNote: updateEntretien.locationNote,
                EnglishNote: updateEntretien.EnglishNote,
                nationalityNote: updateEntretien.nationalityNote,
                competences: updateEntretien.competences
            }
        }

        $http(req).then(function (response) {
            $scope.contentResponseEntretien = response.data.content;
            if (response.data.content != "Le report a ete modifie parfaitement à votre system") {
                console.log("In the error");
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
            url: 'http://192.168.0.16:5000/api/user/add/message/',
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
        if ($scope.updateMessage.titre_message == null) {
            $scope.updateMessage.titre_message = $scope.titreMsg;
        }
        if ($scope.updateMessage.contenu_message == null) {
            $scope.updateMessage.contenu_message = $scope.contenuMsg;
        }


        var req = {
            method: 'POST',
            url: 'http://192.168.0.16:5000/api/candidate/template/email/update',
            responseType: "json",
            data: {
                token: $cookies.get('cookie'),
                title: updateMessage.titre_message,
                content: updateMessage.contenu_message,
            }
        }

        $http(req).then(function (response) {
            $scope.contentResponseMessage = response.data.content;
            if (response.data.content != "Le template d'email a bien ete modifie") {
                console.log("In the error");
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

/*********************  supprimer un message  ********************/

gestionCandidatApp.controller("deleteMessage", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {

    $scope.sendMessageDel = function (deleteMessage) {
        var req = {
            method: 'POST',
            url: 'http://192.168.0.16:5000/api/candidate/template/email/delete',
            responseType: "json",
            data: {
                token: $cookies.get('cookie'),
                title: $scope.selectedMsg,
            }
        }

        $http(req).then(function (response) {
            $scope.contentResponseMessage = response.data.content;
            console.log(response.data.content);

        }, (err) => {
            console.log("ceci est une erreur" + err);
        });
    }
}]);

/****************************************************************/

/*********************  Recherche un candidat  ********************/

gestionCandidatApp.controller("rechercheCandidat", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {

    $scope.sendCandidat = function (candidat) {
        $http.get('http://192.168.0.16:5000/api/user/Candidates/recherche/' + candidat.nom + '/' + $cookies.get('cookie')).then(function (response) {
            $scope.todos = response.data;
            if ($scope.todos != null) {
                if ($scope.todos[0].content != null) {
                    $scope.contentResponse = $scope.todos[0].content;
                    console.log($scope.todos[0].content);

                } else {
                    var nbSelect = 0;
                    for (var i = 0; i < $scope.todos.length; i++) {
                        if ($scope.todos[i].email == $scope.selectedCar) {
                            nbSelect = i;
                            //console.log($scope.todos[i].email);
                        }
                        //console.log($scope.todos[i].email);
                    }
                    $scope.nomCoor = $scope.todos[nbSelect].nom;
                    $scope.prenomCoor = $scope.todos[nbSelect].prenom;
                    $scope.phoneCoor = $scope.todos[nbSelect].phone;
                    $scope.emailCoor = $scope.todos[nbSelect].email;
                    $scope.sexeCoor = $scope.todos[nbSelect].sexe;
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

    $http.get('http://192.168.0.16:5000/api/user/Candidates/rechercheAll/' + $cookies.get('cookie')).then(function (response) {
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
    $http.get('http://192.168.0.16:5000/api/candidate/email/template/titles/' + $cookies.get('cookie') + '/0/200').then(function (response) {
            $scope.todos = response.data;
            $scope.sendMessage = function (message) {
                $http.get('http://192.168.0.16:5000/api/candidate/email/template/titles/' + $cookies.get('cookie') + '/0/200').then(function (response) {
                    $scope.todos = response.data;
                    if ($scope.todos != null) {
                        if ($scope.todos[0].content != null) {
                            console.log($scope.todos[0].content);
                        } else {
                            var nbSelect = 0;
                            for (var i = 0; i < $scope.todos.length; i++) {
                                if ($scope.todos[i].titre_message == $scope.selectedMsg) {
                                    nbSelect = i;
                                }
                            }
                            $scope.titreMsg = $scope.todos[nbSelect].titre_message;
                            $http.get('http://192.168.0.16:5000/api/candidate/template/email/' + $cookies.get('cookie') + '/' + $scope.titreMsg).then(function (response) {
                                $scope.contenuMsg = response.data[0].contenu_message;
                                });
                        }
                    } else {
                        console.log("In the error");
                    }
                }, (err) => {
                    console.log(err);
                });
            }}, (err) => {
            console.log(err);
        });
}]);

/*************************************************************************/

/*
gestionCandidatApp.controller("rechercheCandidat", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {

    var req = {
        method: 'GET',
        url: 'http://192.168.0.16:5000/api/user/Candidates/recherche/',
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