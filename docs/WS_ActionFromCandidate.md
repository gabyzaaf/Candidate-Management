# Web services for search the action from the candidate

1. The first way is to connect on this url  it's a ***GET*** request :

2. The url is : 

***http://localhost:5000/api/candidate/actions/{candidateaction}/{token}***

3. The parameters are : 

|  candidateaction | token   |
|---|---| 
|  string |  string |


4. Sample result : 

``` json
[
    {
        "nom": "zaafrani",
        "prenom": "Gabriel",
        "phone": "065434563",
        "email": "chr.tot@gmail.com",
        "actions": "aRelancerLKD",
        "annee": "2015",
        "lien": "http://google.com",
        "crCall": "cr call",
        "zipcode": "75018",
        "NS": "NS"
    }
]
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



