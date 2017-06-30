# This page will explain how to search candidate not attached to a report :

0. Information : 
You need to have the read access.

1. Web service type : 
The web service type is ***GET***

2. The web service URL : 
***http://localhost:5000/api/Remind/candidate/withoutRapport/{token}***

3. The params : 
| token   |
|---| 
|  string |

The token is the user access.

4. The Sample result : 

```json
[
    {
        "session_id": null,
        "name": "zaafrani",
        "firstname": "Gabriel",
        "emailAdress": "gabriel.zaafrani@toto.com",
        "phone": "065434563",
        "zipcode": "75018",
        "sexe": "M",
        "action": "interne",
        "year": 2015,
        "link": "http://google.com",
        "crCall": "cr call",
        "ns": "NS",
        "email": false,
        "meetingNote": null,
        "linkMeeting": null,
        "xpNote": null,
        "nsNote": null,
        "jobIdeal": null,
        "pisteNote": null,
        "pieCouteNote": null,
        "locationNote": null,
        "englishNote": null,
        "nationalityNote": null,
        "competences": null,
        "crEntretien": null,
        "independant": 0
    },
    {
        "session_id": null,
        "name": "Hand",
        "firstname": "Tom",
        "emailAdress": "Tom.hand@hotmail.com",
        "phone": "065434563",
        "zipcode": "75018",
        "sexe": "M",
        "action": "appellerRemind",
        "year": 2015,
        "link": "http://google.com",
        "crCall": "cr call",
        "ns": "NS",
        "email": false,
        "meetingNote": null,
        "linkMeeting": null,
        "xpNote": null,
        "nsNote": null,
        "jobIdeal": null,
        "pisteNote": null,
        "pieCouteNote": null,
        "locationNote": null,
        "englishNote": null,
        "nationalityNote": null,
        "competences": null,
        "crEntretien": null,
        "independant": 0
    }
]
```
