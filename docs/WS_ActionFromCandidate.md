# Web services for search the action from the candidate

1. The first way is to connect on this url  it's a ***GET*** request :

2. The url is : 

***http://localhost:5000/api/candidate/actions/{action}/{token}***

3. The parameters are : 

|  actions | token   |
|---|---| 
|  string |  string |


4. Sample result : 

``` json
{
    "nom": "Zaafrani",
    "prenom": "David",
    "phone": "0699822435",
    "email": "ga@gmail.com",
    "actions": "enCours",
    "annee": "2016",
    "lien": "http://google.com",
    "crCall": "callSpeedly",
    "NS": "NsTest"
  },
  {
    "nom": "Zaafrani",
    "prenom": "David",
    "phone": "0699822435",
    "email": "gaz@gmail.com",
    "actions": "enCours",
    "annee": "2016",
    "lien": "http://google.com",
    "crCall": "callSpeedly",
    "NS": "NsTest"
  }
```

5. If it's token error : 

``` json
[
  {
    "code": 4,
    "content": "Aucun token ayant ce numero 72 existe veuillez vous identifier",
    "success": false
  }
]
```

6. If the error is about the action : 
``` json
[
  {
    "code": 4,
    "content": "Aucun candidat ne possede vos criteres d'action : el",
    "success": false
  }
]
```

7. For know the action type you need to read the specification.


