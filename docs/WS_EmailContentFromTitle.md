# This page is for describe how to call the webservice for get the email content from a Title : 

1. The URL type is ***GET***

2. The URL adress is : 

***http://localhost:5000/api/candidate/template/email/{token}/{title}***


|  token | title |
|---|---|
|  string |  string |

3. A Sample : 

***http://localhost:5000/api/candidate/template/email/25/f1**

4. The content result : 

```json 
[
    {
        "contenu_message": "ceci est le premier fichier\n"
    }
]
```

5. The error type : 
```json
[
    {
        "code": 5,
        "content": "Vous ne possédez aucun contenu d'email ayant ce titre : f3",
        "success": false
    }
]
```
