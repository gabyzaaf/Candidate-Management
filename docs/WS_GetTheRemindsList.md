# this page will explain how to get the remind list : 

0. Information : 

The user need to have the access to read data.

1. the webservice type:

The webservice type is ***GET***

2. The URL : 
***http://localhost:5000/api/Remind/calendar/remind/informations/{token}***

3. The params : 

| token |
|---| 
|  string |

token : Is the user token.

4. The sample result format : 

```json
[
    {
        "nom": "zaafrani",
        "prenom": "Gabriel",
        "email": "gabriel.zaafrani@toto.com",
        "dates": "6/29/17 12:00:00 AM",
        "zipcode": "75018",
        "actions": "aRelancerLKD"
    },
    {
        "nom": "Hand",
        "prenom": "Tom",
        "email": "Tom.hand@hotmail.com",
        "dates": "6/28/17 12:00:00 AM",
        "zipcode": "75018",
        "actions": "appellerRemind"
    }
]
```

5. Exception : 

If you don't have remind you will get Error :

```json
{
    "code": 1,
    "content": "Il n'existe aucun remind",
    "success": false
}
``` 
