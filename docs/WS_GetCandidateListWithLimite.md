# This page will explain how to get the candidate list between specific limit :

1. Webservice type : 
The web service type is ***GET***

2. The webservice URL is : 

***http://localhost:5000/api/candidate/list/{limite1}/{limite2}/{token}***

3. The result : 

```json
[
    {
        "nom": "zaafrani",
        "prenom": "Gabriel",
        "sexe": "M",
        "phone": "065434563",
        "email": "chr.totoz@gmail.com"
    },
    {
        "nom": "zaafrani",
        "prenom": "Gabriel",
        "sexe": "M",
        "phone": "065434563",
        "email": "chris.totoz@gmail.com"
    }
]
```

4. Error type 1 :

```json
[
    {
        "code": 5,
        "content": "La limite1 ne peux pas etre supérieur à la limite 2",
        "success": false
    }
]
```

5. Error type 2 : 

```json
[
    {
        "code": 5,
        "content": "Vous n'avez aucun candidat entre les limites 10 et 200",
        "success": false
    }
]
```