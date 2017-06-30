# This page is for explain how to search candidate :

0. Information : 
The candidate to search need to have a report inserted inside the database.

1. Webservice type : 
The webService is ***GET***

2. The URL is  : 
***http://localhost:5000/api/user/Candidates/recherche/{name}/{token}***

3. The params : 

|  name | token |
|---|---| 
|  string |  string |

name : Is the candidate Name.
token : Is the user token.

4. Success result : 

```json
[
    {
        "nom": "LAPERT",
        "prenom": "Christian",
        "sexe": "H",
        "phone": "760232307",
        "zipcode": "",
        "actions": "interne",
        "annee": "2015",
        "lien": "http://google.com",
        "crCall": "cr call",
        "NS": "NSsieufq",
        "approche_email": "True",
        "email": "cl@esgi.com",
        "note": "12",
        "link": "it's a link",
        "xpNote": "it's a xpNote",
        "nsNote": "ns note",
        "jobIdealNote": "jobIdealNote",
        "pisteNote": "piste note",
        "pieCouteNote": "pie coute note",
        "locationNote": "location note",
        "EnglishNote": "750",
        "nationalityNote": "French",
        "competences": "java programmer"
    }
]
```

5. The Wrong response if you indicate a name not exist :

```json
[
    {
        "code": 3,
        "content": "Aucun candidat ne possede vos criteres de recherche",
        "success": false
    }
]
```