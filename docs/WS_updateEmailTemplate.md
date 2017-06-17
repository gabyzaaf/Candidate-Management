# this page explait how to update the email template : 

1. Information
you need to have the access to add and update the candidate inside the application.

2. The URL type is ***POST***

3. The URL link is  : 
***http://localhost:5000/api/candidate/template/email/update***

4. The Json parameters are : 

```json
{
	"token":"25",
	"title":"f1",
	"content":"this is a content"
}
```
|  token | title   | content   |
|---|---|---| 
|  string |  string | string|

The title need to be reference for the name file.


5. The success result is  : 

```json
{
    "code": 4,
    "content": "Le template d'email a bien ete modifie",
    "success": true
}
```

6. The title fail result is : 

```json
[
    {
        "code": 5,
        "content": "Aucun template existe avec votre titre : f22",
        "success": false
    }
]
```

7. The content fail result (if content is empty) : 
```json
[
    {
        "code": 5,
        "content": "Le chemin du fichier est vide",
        "success": false
    }
]
```
The reason of this erreur is because of the Lazy Loading pattern when we call the getContent function he will read the file content present in the path.

