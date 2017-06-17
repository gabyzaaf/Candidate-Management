# This page explain how to delete an Email template.

1. Information : 
You need to have the permission to delete inside the application.

If you don't have the access you will get this error : 

```json
[
    {
        "code": 5,
        "content": " Vous n'avez pas les droits necessaire pour effectuer une suppression",
        "success": false
    }
]
```

2. The URL type :
The URL type is ***POST***


3. The URL Link is : 
***http://localhost:5000/api/candidate/template/email/delete***

4. The Json parameters : 

```json
{
	"token":"25",
	"title":"f2"
}
```

|  token | title   |
|---|---|---| 
|  string |  string |

Title represent the file title of your email template *** WITHOUT the extension ***

5. Json success result is : 

```json
{
    "code": 4,
    "content": "Le template d'email ayant le titre f2 à bien ete supprime",
    "success": true
}
```
6. If title is empty or with a wrong name:

```json
[
    {
        "code": 5,
        "content": "Aucun template existe avec votre titre : wrongNameTemplate",
        "success": false
    }
]
``` 

