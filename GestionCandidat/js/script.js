'use strict';

var gestionCandidatApp = angular.module('gestionCandidatApp', ['ngRoute', 'ngCookies']);

var adresseIP = "192.168.126.146:5000";

/********************   Prediction machine learning azure   ********************/

gestionCandidatApp.controller('predictionAzure', ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {
    $scope.sendValueForAzure = function (prediction) {
        try {
            $scope.resultatAzure = "";
            $scope.probAzure = "";
            $scope.satisfaction_level = prediction.satisfaction_level;
            if ($scope.satisfaction_level == null || prediction.last_evaluation == null || prediction.number_project == null  || prediction.average_montly_hours == null || prediction.time_spend_company == null || prediction.Work_accident == null || prediction.promotion_last_5years == null || prediction.sales == null || prediction.salary == null) {
                console.log("erreur : Tous les champs ne sont pas remplis");
            } else {
                var req = {
                    method: 'POST',
                    url: 'http://' + adresseIP + '/api/user/MlCandidate/prediction',
                    responseType: "json",
                    data: {
                        satisfaction_level: prediction.satisfaction_level,
                        last_evaluation: prediction.last_evaluation,
                        number_project: prediction.number_project,
                        average_montly_hours: prediction.average_montly_hours,
                        time_spend_company: prediction.time_spend_company,
                        Work_accident: prediction.Work_accident,
                        promotion_last_5years: prediction.promotion_last_5years,
                        sales: prediction.sales,
                        salary: prediction.salary
                    }
                }

                $http(req).then(function (response) {
                    $scope.resultatAzure = response.data.content.substring(0, 1);
                    $scope.probAzure = response.data.content.substring(3);
                    $scope.probAzurePourc = response.data.content.substring(3)+'%';
                    if ($scope.probAzure == "]]") {
                        $scope.resultatAzure = "Les données saisies sont incohérentes";
                        $scope.probAzure = "Les données saisies sont incohérentes";
                    } else {
                        if ($scope.resultatAzure == 1) {
                            $scope.resultatAzure = "Salarié quitte l'entreprise";
                        } else{
                            $scope.resultatAzure = "Salarié ne quitte pas l'entreprise";
                        }

                        if ($scope.probAzure >= 0 && $scope.probAzure < 34) {
                            $scope.probAzure = "Faible " + "(" + $scope.probAzurePourc + ")";
                        } else if ($scope.probAzure >= 34 && $scope.probAzure < 67) {
                            $scope.probAzure = "Moyen " + "(" + $scope.probAzurePourc + ")";
                        } else {
                            $scope.probAzure = "Haut " + "(" + $scope.probAzurePourc + ")";
                        }
                    }

                }, (err) => {
                    console.log("ceci est une erreur" + err);
                });
            }
        }
        catch(e){
            console.log("Remplir les champs pour la prédiction");
        }

    }
}]);

/*******************************************************************/

/********************   Statistiques Candidat   ********************/

gestionCandidatApp.controller('GraphCtrl', ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {
    $http.get('http://' + adresseIP + '/api/Remind/stat/mlcandidate/sales/' + $cookies.get('cookie')).then(function (response) {
        /*$scope.selectedSect = "ko";
        $scope.sendSect = function () {
            $scope.selectedSect = selectedSect;
            console.log($scope.selectedSect);
        }*/
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
        $scope.chart5 = null;

        $scope.config = {};
        

        $scope.typeOptions = ["line", "bar", "spline", "step", "area", "area-step", "area-spline"];

        $scope.config.type1 = $scope.typeOptions[0];
        $scope.config.type2 = $scope.typeOptions[1];

        $scope.showGraph = function () {
            $scope.graph1 = "Graph 1 : Graphique sur les promotions et les nombres de projets";
            $scope.config.data1 = $scope.number_project;
            $scope.config.data2 = $scope.promotion_last_5years;
            $scope.config.type1 = $scope.typeOptions[6];
            $scope.config.type2 = $scope.typeOptions[2];
            var config = {};
            config.bindto = '#chart';
            config.zoom = { "enabled": "true" };
            config.data = {};
            config.data.json = {};
            config.data.json.number_project = $scope.config.data1.split(";");
            config.data.json.promotion_last_5years = $scope.config.data2.split(";");
            config.axis = { "y": { "label": { "text": "Number of items", "position": "outer-middle" } } };
            config.data.types = { "number_project": $scope.config.type1, "promotion_last_5years": $scope.config.type2 };
            $scope.chart = c3.generate(config);
        }

        $scope.showGraph2 = function () {
            $scope.graph2 = "Graph 2 : Graphique sur les niveaux de satisfaction du salarié et des promotions affectés durant les 5 dernières années";
            $scope.config.data1 = $scope.satisfaction_level;
            $scope.config.data2 = $scope.promotion_last_5years;
            $scope.config.type1 = $scope.typeOptions[6];
            $scope.config.type2 = $scope.typeOptions[2];
            var config = {};
            config.bindto = '#chart2';
            config.zoom = { "enabled": "true" };
            config.data = {};
            config.data.json = {};
            config.data.json.satisfaction_level = $scope.config.data1.split(";");
            config.data.json.promotion_last_5years = $scope.config.data2.split(";");
            config.axis = { "y": { "label": { "text": "Number of items", "position": "outer-middle" } } };
            config.data.types = { "satisfaction_level": $scope.config.type1, "promotion_last_5years": $scope.config.type2 };
            $scope.chart2 = c3.generate(config);
        }

        $scope.showGraph3 = function () {
            $scope.graph3 = "Graph 3 : Graphique sur le nombre de projets et l'évaluation du salarié";
            $scope.config.data1 = $scope.number_project;
            $scope.config.data2 = $scope.last_evaluation;
            $scope.config.type1 = $scope.typeOptions[6];
            $scope.config.type2 = $scope.typeOptions[2];
            var config = {};
            config.bindto = '#chart3';
            config.zoom = { "enabled": "true" };
            config.data = {};
            config.data.json = {};
            config.data.json.number_project = $scope.config.data1.split(";");
            config.data.json.last_evaluation = $scope.config.data2.split(";");
            config.axis = { "y": { "label": { "text": "Number of items", "position": "outer-middle" } } };
            config.data.types = { "number_project": $scope.config.type1, "last_evaluation": $scope.config.type2 };
            $scope.chart3 = c3.generate(config);
        }

        $scope.showGraph4 = function () {
            $scope.graph4 = "Graph 4 : Graphique sur la satisfaction du salarié et le nombre de projets qu'il a effectué";
            $scope.config.data1 = $scope.satisfaction_level;
            $scope.config.data2 = $scope.number_project;
            $scope.config.type1 = $scope.typeOptions[6];
            $scope.config.type2 = $scope.typeOptions[2];
            var config = {};
            config.bindto = '#chart4';
            config.zoom = { "enabled": "true" };
            config.data = {};
            config.data.json = {};
            config.data.json.satisfaction_level = $scope.config.data1.split(";");
            config.data.json.number_project = $scope.config.data2.split(";");
            config.axis = { "y": { "label": { "text": "Number of items", "position": "outer-middle" } } };
            config.data.types = { "satisfaction_level": $scope.config.type1, "number_project": $scope.config.type2 };
            $scope.chart4 = c3.generate(config);
        }

        $scope.showGraph5 = function () {
            $scope.graph5 = "Graph 5 : Ensemble des données";
            $scope.config.data1 = $scope.satisfaction_level;
            $scope.config.data2 = $scope.last_evaluation;
            $scope.config.data3 = $scope.number_project;
            //$scope.config.data4 = $scope.average_montly_hours;
            $scope.config.data5 = $scope.time_spend_company;
            //$scope.config.data6 = $scope.Work_accident;
            //$scope.config.data7 = $scope.left_work;
            $scope.config.data8 = $scope.promotion_last_5years;
            $scope.config.type1 = $scope.typeOptions[0];
            $scope.config.type2 = $scope.typeOptions[0];
            $scope.config.type3 = $scope.typeOptions[0];
            //$scope.config.type4 = $scope.typeOptions[0];
            $scope.config.type5 = $scope.typeOptions[0];
            //$scope.config.type6 = $scope.typeOptions[0];
            //$scope.config.type7 = $scope.typeOptions[0];
            $scope.config.type8 = $scope.typeOptions[0];
            var config = {};
            config.bindto = '#chart5';
            config.zoom = { "enabled": "true" };
            config.data = {};
            config.data.json = {};
            config.data.json.satisfaction_level = $scope.config.data1.split(";");
            config.data.json.last_evaluation = $scope.config.data2.split(";");
            config.data.json.number_project = $scope.config.data3.split(";");
            //config.data.json.average_montly_hours = $scope.config.data4.split(";");
            config.data.json.time_spend_company = $scope.config.data5.split(";");
            //config.data.json.Work_accident = $scope.config.data6.split(";");
            //config.data.json.left_work = $scope.config.data7.split(";");
            config.data.json.promotion_last_5years = $scope.config.data8.split(";");
            config.axis = { "y": { "label": { "text": "Number of items", "position": "outer-middle" } } };
            config.data.types = { "satisfaction_level": $scope.config.type1, "last_evaluation": $scope.config.type2, "number_project": $scope.config.type3, /*"average_montly_hours": $scope.config.type4,*/ "time_spend_company": $scope.config.type5, /*"Work_accident": $scope.config.type6, "left_work": $scope.config.type7,*/ "promotion_last_5years": $scope.config.type8 };
            $scope.chart5 = c3.generate(config);
        }

    });
}]);

/********************************************************/

/********************   Se déconnecter   ********************/

gestionCandidatApp.controller('seDeconnecter', ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {
    $scope.sendDeconnexion = function () {
        $http.get('http://' + adresseIP + '/api/User/Disconnect/' + $cookies.get('cookie')).then(function (response) {
            $cookies.put('cookie', "null");
            if ($cookies.get('cookie') == "null") {
                $window.location.href = 'index.html';
            }
        })
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
            url: 'http://' + adresseIP + '/api/user/admin/auth/',
            responseType: "json",
            data: { email: utilisateur.email, password: utilisateur.password }
        }

        $http(req).then(function (response) {
            if (response.data.content != null) {
                //console.log("E-mail ou mot de passe incorrect");
                $scope.errtxt = response.data.content;
            } else {
                //console.log("En attente du cookie");
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

gestionCandidatApp.controller("addCandidate", ['$scope', '$cookies', '$http', '$window', '$timeout', function ($scope, $cookies, $http, $window, $timeout) {
    $scope.sendEntry = function (candidat) {
        try {
            $scope.Name = candidat.Name;
            $scope.Firstname = candidat.Firstname;
            $scope.emailAdress = candidat.emailAdress;
            $scope.phone = candidat.phone;
            $scope.sexe = candidat.sexe;
            $scope.zipcode = candidat.cp;
            $scope.action = candidat.action;
            $scope.plugins = candidat.plugins;


            /******************** Regex champ nom ********************/
            try{
                var regexNom = $scope.Name.match(/[A-Za-z]+/g);
                if (regexNom[0] != $scope.Name) {
                    $scope.erreurNormeNom = "Le champ nom ne respect pas la norme d'un nom";
                    $timeout(function () { $scope.erreurNormeNom = ""; }, 5000);
                }
            } catch (e) {
                $scope.erreurNormeNom = "Le champ nom ne respect pas la norme d'un nom";
                $timeout(function () { $scope.erreurNormeNom = ""; }, 5000);
            }
            /********************************************************/

            /******************** Regex champ prenom ********************/
            try {
                var regexFirstname = $scope.Firstname.match(/[A-Za-z]+/g);
                if (regexFirstname[0] != $scope.Firstname) {
                    $scope.erreurNormeFirstname = "Le champ prenom ne respect pas la norme d'un prénom";
                    $timeout(function () { $scope.erreurNormeFirstname = ""; }, 5000);
                }
            } catch (e) {
                $scope.erreurNormeFirstname = "Le champ prénom ne respect pas la norme d'un prénom";
                $timeout(function () { $scope.erreurNormeFirstname = ""; }, 5000);
            }
            /********************************************************/

            /******************** Controle champ action ********************/
            try {
                if ($scope.action != "interne" && $scope.action != "enCours" && $scope.action != "RN" && $scope.action != "trop relance" && $scope.action != "freelance" && $scope.action != "appellerRemind" && $scope.action != "aRelancerMail" && $scope.action != "aRelancerLKD" && $scope.action != "PAERemind" && $scope.action != "HcJunior" && $scope.action != "HcLangue" && $scope.action != "HCGeo" && $scope.action != "HCSenior" && $scope.action != "HCPasDev" && $scope.action != "HCMSFT" && $scope.action != "HCBacMoins5") {
                    $scope.erreurNormeAction = "Le champ action ne respect pas la norme d'une action";
                    $timeout(function () { $scope.erreurNormeAction = ""; }, 5000);
                }
            } catch (e) {
                $scope.erreurNormeAction = "Le champ action ne respect pas la norme d'une action";
                $timeout(function () { $scope.erreurNormeAction = ""; }, 5000);
            }
            /********************************************************/

            /******************** Controle champ sexe ********************/
            try {
                if ($scope.sexe != "H" && $scope.sexe != "F") {
                    $scope.erreurNormeSexe = "Le champ sexe ne respect pas la norme";
                    $timeout(function () { $scope.erreurNormeSexe = ""; }, 5000);
                }
            } catch (e) {
                $scope.erreurNormeSexe = "Le champ sexe ne respect pas la norme";
                $timeout(function () { $scope.erreurNormeSexe = ""; }, 5000);
            }
            /********************************************************/

            
            if ($scope.Name == null || $scope.Firstname == null || $scope.emailAdress == null || $scope.phone == null || $scope.sexe == null || $scope.action == null || $scope.zipcode == null || $scope.plugins == null) {
                $scope.erreurChamps = "Tous les champs obligatoires (*) ne sont pas remplis ou les normes ne sont pas respectées";
                $timeout(function () { $scope.erreurChamps = ""; }, 5000);
                
            } else {

                var req = {
                    method: 'POST',
                    url: 'http://' + adresseIP + '/api/user/add/candidat/',
                    responseType: "json",
                    data: {
                        session_id: $cookies.get('cookie'),
                        Name: candidat.Name,
                        Firstname: candidat.Firstname,
                        emailAdress: candidat.emailAdress,
                        phone: candidat.phone,
                        sexe: candidat.sexe,
                        action: candidat.action,
                        zipcode: candidat.cp,
                        year: candidat.year,
                        link: candidat.link,
                        crCall: candidat.crCall,
                        ns: candidat.note,
                        pluginType: candidat.plugins
                    }
                }

                $http(req).then(function (response) {
                    $scope.contentResponse = response.data.content;
                    $scope.contentResponseGreen = "";
                    if (response.data.content == "Le candidat est deja existant dans votre systeme" || response.data.content == "Le token n'existe pas ") {
                        $timeout(function () { $scope.contentResponse = ""; }, 5000);
                    } else {
                        $scope.contentResponseGreen = $scope.contentResponse;
                        $timeout(function () { $scope.contentResponseGreen = ""; }, 5000);
                        $scope.contentResponse = "";
                    }
                }, (err) => {
                    console.log("ceci est une erreur" + err);
                });
            }
        } catch (e) {
            console.log(e.message);
        }

    }
}]);

/****************************************************************/

/*********************  Ajouter un entretien  ********************/

gestionCandidatApp.controller("addEntretien", ['$scope', '$cookies', '$http', '$window', '$timeout', function ($scope, $cookies, $http, $window, $timeout) {
    $scope.sendEntry = function (entretien) {
        try {
            $scope.emailCandidat = $scope.selectedCar;
            $scope.note = entretien.note;
            if ($scope.emailCandidat == null || $scope.note == null) {
                $scope.erreurEntretien = "Les champs obligatoires (*) ne sont pas remplie";
                $timeout(function () { $scope.erreurEntretien = ""; }, 5000);
            } else {
                var req = {
                    method: 'POST',
                    url: 'http://' + adresseIP + '/api/user/add/candidat/report',
                    responseType: "json",
                    data: {
                        sessionId: $cookies.get('cookie'),
                        emailCandidat: $scope.selectedCar,
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
                        $scope.contentResponseGreen = "";
                        if (response.data.content != "Le report a ete ajoute parfaitement à votre system" || response.data.content == "Le token n'existe pas ") {
                        $timeout(function () { $scope.contentResponse = ""; }, 5000);
                    } else {
                            $scope.contentResponseGreen = $scope.contentResponse;
                            $scope.contentResponse = "";
                            $timeout(function () { $scope.contentResponseGreen = ""; window.location.reload();}, 3000);
                    }


                }, (err) => {
                    console.log("ceci est une erreur" + err);
                });
            }
            
        } catch (e) {
            console.log(e.message);
        }
        
    }
}]);

/****************************************************************/

/*********************  Modifier un candidat  ********************/

gestionCandidatApp.controller("updateCandidate", ['$scope', '$cookies', '$http', '$window', '$timeout', function ($scope, $cookies, $http, $window, $timeout) {

    $scope.sendEntry = function (updateCandidate) {
        try{
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
            /******************** Regex champ nom ********************/
                try {
                    var regexNom = $scope.updateCandidate.nom.match(/[A-Za-z]+/g);
                    if (regexNom[0] != $scope.updateCandidate.nom) {
                        $scope.erreurNormeNom = "Le champ nom ne respect pas la norme d'un nom";
                        $timeout(function () { $scope.erreurNormeNom = ""; }, 5000);
                    }
                } catch (e) {
                    $scope.erreurNormeNom = "Le champ nom ne respect pas la norme d'un nom";
                    $timeout(function () { $scope.erreurNormeNom = ""; }, 5000);
                }
            /********************************************************/

            /******************** Regex champ prenom ********************/
                try {
                    var regexFirstname = updateCandidate.prenom.match(/[A-Za-z]+/g);
                    if (regexFirstname[0] != updateCandidate.prenom) {
                        $scope.erreurNormeFirstname = "Le champ prénom ne respect pas la norme d'un prénom";
                        $timeout(function () { $scope.erreurNormeFirstname = ""; }, 5000);
                    }
                } catch (e) {
                    $scope.erreurNormeFirstname = "Le champ prénom ne respect pas la norme d'un prénom";
                    $timeout(function () { $scope.erreurNormeFirstname = ""; }, 5000);
                }
            /********************************************************/

            /******************** Controle champ action ********************/
                try {
                    if ($scope.updateCandidate.action != "interne" && $scope.updateCandidate.action != "enCours" && $scope.updateCandidate.action != "RN" && $scope.updateCandidate.action != "trop relance" && $scope.updateCandidate.action != "freelance" && $scope.updateCandidate.action != "appellerRemind" && $scope.updateCandidate.action != "aRelancerMail" && $scope.updateCandidate.action != "aRelancerLKD" && $scope.updateCandidate.action != "PAERemind" && $scope.updateCandidate.action != "HcJunior" && $scope.updateCandidate.action != "HcLangue" && $scope.updateCandidate.action != "HCGeo" && $scope.updateCandidate.action != "HCSenior" && $scope.updateCandidate.action != "HCPasDev" && $scope.updateCandidate.action != "HCMSFT" && $scope.updateCandidate.action != "HCBacMoins5") {
                        $scope.erreurNormeAction = "Le champ action ne respect pas la norme d'une action";
                        $timeout(function () { $scope.erreurNormeAction = ""; }, 5000);
                    }
                } catch (e) {
                    $scope.erreurNormeAction = "Le champ action ne respect pas la norme d'une action";
                    $timeout(function () { $scope.erreurNormeAction = ""; }, 5000);
                }
            /********************************************************/

            /******************** Controle champ sexe ********************/
                try {
                    if ($scope.updateCandidate.sexe != "H" && $scope.updateCandidate.sexe != "F") {
                        $scope.erreurNormeSexe = "Le champ sexe ne respect pas la norme";
                        $timeout(function () { $scope.erreurNormeSexe = ""; }, 5000);
                    }
                } catch (e) {
                    $scope.erreurNormeSexe = "Le champ sexe ne respect pas la norme";
                    $timeout(function () { $scope.erreurNormeSexe = ""; }, 5000);
                }
            /********************************************************/


            var req = {
                method: 'POST',
                url: 'http://' + adresseIP + '/api/user/update/candidat/',
                responseType: "json",
                data: {
                    session_id: $cookies.get('cookie'),
                    Name: updateCandidate.nom,
                    Firstname: updateCandidate.prenom,
                    emailAdress: updateCandidate.email,
                    zipcode: updateCandidate.cp,
                    phone: updateCandidate.telephone,
                    sexe: updateCandidate.sexe,
                    action: updateCandidate.action,
                    year: updateCandidate.anneediplome,
                    link: updateCandidate.url,
                    crCall: updateCandidate.cr,
                    NS: updateCandidate.note,
                    pluginType: "email"
                }
            }

            $http(req).then(function (response) {
                $scope.contentResponse = response.data.content;
                $scope.contentResponseGreen = "";
                if (response.data.content == "Le candidat n'est pas existant dans votre systeme, veuillez le creer" || response.data.content == "Le token n'existe pas ") {
                    $timeout(function () { $scope.contentResponse = ""; }, 4000);
                } else {
                    $scope.contentResponse = "";
                    $scope.contentResponseGreen = response.data.content;
                    $timeout(function () { $scope.contentResponseGreen = ""; }, 4000);
                }


            }, (err) => {
                console.log("ceci est une erreur" + err);
            });
        }catch(e){
            console.log("Aucun champ n'a été modifié");
        }
    }
}]);

/****************************************************************/

/*********************  Modifier un entretien  ********************/

gestionCandidatApp.controller("updateEntretien", ['$scope', '$cookies', '$http', '$window', '$timeout', function ($scope, $cookies, $http, $window, $timeout) {

    $scope.sendEntry = function (updateEntretien) {
        try{
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
                url: 'http://' + adresseIP + '/api/user/update/candidat/report',
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
                $scope.contentResponseEntretienGreen = "";
                
                if (response.data.content != "Le report a ete modifie parfaitement à votre system" || response.data.content == "Le token n'existe pas ") {
                    $timeout(function () { $scope.contentResponseEntretien = ""; }, 4000);
                } else {
                    $scope.contentResponseEntretienGreen = response.data.content;
                    $scope.contentResponseEntretien = "";
                    $timeout(function () { $scope.contentResponseEntretienGreen = ""; }, 4000);
                }

            }, (err) => {
                console.log("ceci est une erreur" + err);
            });
        } catch (e) {
            console.log(e.message);
        }
    }
}]);

/****************************************************************/

/*********************  supprimer un candidat  ********************/

gestionCandidatApp.controller("deleteCandidate", ['$scope', '$cookies', '$http', '$window', '$timeout', function ($scope, $cookies, $http, $window, $timeout) {

    $scope.sendCandidateDel = function (deleteMessage) {
        var req = {
            method: 'POST',
            url: 'http://' + adresseIP + '/api/candidate/delete/byEmail',
            responseType: "json",
            data: {
                token: $cookies.get('cookie'),
                email: $scope.selectedCand.email
            }
        }

        $http(req).then(function (response) {
            $scope.contentResponseMessage = response.data.content;
            $scope.contentResponseMessageGreen = "";
            if (response.data.content != "Le Candidat a bien été supprimé") {
                $timeout(function () {
                    $scope.contentResponseMessage = "";
                }, 4000);
            } else {
                $scope.contentResponseMessage = "";
                $scope.contentResponseMessageGreen = response.data.content;
                $timeout(function () {
                    $scope.contentResponseMessageGreen = "";
                    window.location.reload();
                }, 3000);
            }
        }, (err) => {
            console.log("ceci est une erreur" + err);
        });
    }
}]);

/****************************************************************/


/*********************  Ajouter un message  ********************/
/*
gestionCandidatApp.controller("addMessage", ['$scope', '$cookies', '$http', '$window', '$timeout', function ($scope, $cookies, $http, $window, $timeout) {
    $scope.sendEntry = function (message) {
        var req = {
            method: 'POST',
            url: 'http://' + adresseIP + '/api/user/add/message/',
            responseType: "json",
            data: {
                session_id: $cookies.get('cookie'),
                titre: message.titre,
                contenu: message.contenu,
            }
        }

        $http(req).then(function (response) {
            $scope.contentResponse = response.data.content;
            $timeout(function () { $scope.contentResponse = ""; }, 3000);
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
*/
/****************************************************************/

/*********************  modifier un message  ********************/

gestionCandidatApp.controller("updateMessage", ['$scope', '$cookies', '$http', '$window', "$timeout", function ($scope, $cookies, $http, $window, $timeout) {

    $scope.sendEntry = function (updateMessage) {
        if ($scope.updateMessage.titre_message == null) {
            $scope.updateMessage.titre_message = $scope.titreMsg;
        }
        if ($scope.updateMessage.contenu_message == null) {
            $scope.updateMessage.contenu_message = $scope.contenuMsg;
        }


        var req = {
            method: 'POST',
            url: 'http://' + adresseIP + '/api/candidate/template/email/update',
            responseType: "json",
            data: {
                token: $cookies.get('cookie'),
                title: updateMessage.titre_message,
                content: updateMessage.contenu_message,
            }
        }

        $http(req).then(function (response) {
            $scope.contentResponseMessage = response.data.content;
            $scope.contentResponseMessageGreen = "";
            if (response.data.content != "Le template d'email a bien ete modifie") {
                $timeout(function () { $scope.contentResponseMessage = ""; }, 4000);
            } else {
                $scope.contentResponseMessage = "";
                $scope.contentResponseMessageGreen = response.data.content;
                $timeout(function () { $scope.contentResponseMessageGreen = ""; }, 4000);
            }

        }, (err) => {
            console.log("ceci est une erreur" + err);
        });
    }
}]);

/****************************************************************/

/*********************  supprimer un message  ********************/

gestionCandidatApp.controller("deleteMessage", ['$scope', '$cookies', '$http', '$window', '$timeout', function ($scope, $cookies, $http, $window, $timeout) {

    $scope.sendMessageDel = function (deleteMessage) {
        var req = {
            method: 'POST',
            url: 'http://' + adresseIP + '/api/candidate/template/email/delete',
            responseType: "json",
            data: {
                token: $cookies.get('cookie'),
                title: $scope.selectedMsg,
            }
        }

        $http(req).then(function (response) {
            $scope.contentResponseMessageGreen = response.data.content;
            $timeout(function () {
                $scope.contentResponseMessageGreen = "";
                window.location.reload();
            }, 4000);
        }, (err) => {
            console.log("ceci est une erreur" + err);
        });
    }
}]);

/****************************************************************/

/*********************  Recherche un candidat  ********************/

gestionCandidatApp.controller("rechercheCandidat", ['$scope', '$cookies', '$http', '$window', '$timeout', function ($scope, $cookies, $http, $window, $timeout) {

    $scope.sendCandidat = function (candidat) {
        if (candidat.nom == "") {
            console.log("le champ recherche est vide ou n'est pas rempli")
        } else {
            $http.get('http://' + adresseIP + '/api/user/Candidates/recherche/' + candidat.nom + '/' + $cookies.get('cookie')).then(function (response) {
                $scope.todos = response.data;
                $scope.resultatTrouve = 0;
                if ($scope.todos != null) {
                    if ($scope.todos[0].content != null) {
                        $scope.contentResponse = $scope.todos[0].content;
                        $timeout(function () { $scope.contentResponse = ""; }, 3000);
                    } else {
                        $scope.contentResponse = "";
                        var nbSelect = 0;
                        $scope.resultatTrouve = $scope.todos.length;
                        for (var i = 0; i < $scope.todos.length; i++) {
                            if ($scope.todos[i].email == $scope.selectedCar) {
                                nbSelect = i;
                                //console.log($scope.todos[i].email);
                            }
                        }
                        $scope.nomCoor = $scope.todos[nbSelect].nom;
                        $scope.prenomCoor = $scope.todos[nbSelect].prenom;
                        $scope.phoneCoor = $scope.todos[nbSelect].phone;
                        $scope.emailCoor = $scope.todos[nbSelect].email;
                        $scope.sexeCoor = $scope.todos[nbSelect].sexe;
                        $scope.cpCoor = $scope.todos[nbSelect].zipcode;
                        $scope.actionsCoor = $scope.todos[nbSelect].actions;
                        $scope.anneeCoor = $scope.todos[nbSelect].annee;
                        $scope.lienCoor = $scope.todos[nbSelect].lien;
                        $scope.crCallCoor = $scope.todos[nbSelect].crCall;
                        $scope.NSCoor = $scope.todos[nbSelect].NS;
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
    }
}]);

/*************************************************************************/

/*********************  Recherche tous les candidats ********************/

gestionCandidatApp.controller("rechercheAllCand", ['$scope', '$cookies', '$http', '$window', '$timeout', function ($scope, $cookies, $http, $window, $timeout) {

    $http.get('http://' + adresseIP + '/api/candidate/list/0/200/' + $cookies.get('cookie')).then(function (response) {
        try {
            $scope.todos = response.data;
            if ($scope.todos[0].content != null) {
                $scope.erreurRechercheAll = "Aucun aucun candidat n'existe";
                $scope.todos = "";
            }
        } catch (e) {
            $scope.erreurRechercheAll = "Aucun aucun candidat n'existe";
        }
    }, (err) => {
        console.log(err);
    });
}]);

/********************************************************************/

/*********************  Recherche tous les candidats qui n'ont pas de fiche entretien ********************/

gestionCandidatApp.controller("rechercheAllCandidat", ['$scope', '$cookies', '$http', '$window', '$timeout', function ($scope, $cookies, $http, $window, $timeout) {

    $http.get('http://' + adresseIP + '/api/Remind/candidate/withoutRapport/' + $cookies.get('cookie')).then(function (response) {
        try {
            $scope.todos = response.data;
            $scope.selectedCar = $cookies.get('id');
            if ($scope.todos != null) {
                if ($scope.todos[0].content != null) {
                    $scope.contentResponse = $scope.todos[0].content;
                } else {
                    var nbSelect = 0;
                    for (var i = 0; i < $scope.todos.length; i++) {
                        if ($scope.todos[i].phone == $scope.selectedCar) {
                            nbSelect = i;
                        }
                    }
                    $scope.id = $scope.todos[nbSelect].id;
                    $scope.nomCoor = $scope.todos[nbSelect].name;
                    $scope.prenomCoor = $scope.todos[nbSelect].firstname;
                    $scope.phoneCoor = $scope.todos[nbSelect].phone;
                    $scope.emailCoor = $scope.todos[nbSelect].emailAdress;
                }
            } else {
                console.log("In the error");
            }
        } catch (e) {
            $scope.erreurRechercheAll = "Tous les candidats possède déjà une fiche entretien";
        }
    }, (err) => {
            console.log(err);
        });
}]);

/***************************************************************************************************/

/*********************  Recherche tous les messages  ********************/

gestionCandidatApp.controller("rechercheAllMessage", ['$scope', '$cookies', '$http', '$window', '$timeout', function ($scope, $cookies, $http, $window, $timeout) {
    $http.get('http://' + adresseIP + '/api/candidate/email/template/titles/' + $cookies.get('cookie') + '/0/200').then(function (response) {
            $scope.todos = response.data;
            $scope.sendMessage = function (message) {
                $http.get('http://' + adresseIP + '/api/candidate/email/template/titles/' + $cookies.get('cookie') + '/0/200').then(function (response) {
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
                            $http.get('http://' + adresseIP + '/api/candidate/template/email/' + $cookies.get('cookie') + '/' + $scope.titreMsg).then(function (response) {
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

/**********************  Recherche tous les reminds  *********************/

gestionCandidatApp.controller("rechercheAllReminds", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {

    $http.get('http://' + adresseIP + '/api/Remind/calendar/remind/informations/' + $cookies.get('cookie')).then(function (response) {
        try{
            $scope.todos = response.data;
            if ($scope.todos != null) {
                if ($scope.todos.content != null) {
                    $scope.contentResponse = $scope.todos.content;
                } else {
                    $scope.stripDay = function (dates) {
                        return dates.substring(0, 2);
                    }
                    $scope.stripMonth = function (dates) {
                        return dates.substring(3, 5);
                    }
                    $scope.stripYear = function (dates) {
                        return dates.substring(6, 10);
                    }
                }
            } else {
                console.log("In the error");
            }
        } catch (e) {
            console.log(e.message);
        }
        
    }, (err) => {
        console.log(err);
    });
}]);

/*************************************************************************/

/**********************  Recherche tous les plugins  *********************/

gestionCandidatApp.controller("rechercheAllPlugins", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {

    $http.get('http://' + adresseIP + '/api/Remind/display/plugins/list').then(function (response) {
        $scope.todos = response.data;
        if ($scope.todos != null) {
            if ($scope.todos[0].content != null) {
                console.log($scope.todos[0].content);
            }
        } else {
            console.log("In the error");
        }
    }, (err) => {
        console.log(err);
    });
}]);

/*************************************************************************/

gestionCandidatApp.directive('stringToNumber', function () {
    return {
        require: 'ngModel',
        link: function(scope, element, attrs, ngModel) {
            ngModel.$parsers.push(function(value) {
                return '' + value;
            });
            ngModel.$formatters.push(function(value) {
                return parseFloat(value, 10);
            });
        }
    };
});


/*
gestionCandidatApp.controller("rechercheCandidat", ['$scope', '$cookies', '$http', '$window', function ($scope, $cookies, $http, $window) {

    var req = {
        method: 'GET',
        url: 'http://' + adresseIP + '/api/user/Candidates/recherche/',
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