# This page will explain how to delete a candidate by this email adress .

0. Information : 
You need to have the permission to delete a candidate inside the system.

1. The webservice type is ***POST***


2. The URL is : 

***http://localhost:5000/api/candidate/delete/byEmail***

3. The parameters are : 

```json
{
	"token":"39",
	"email":"chr.toto@gmail.com"
}
```

4. Parameters : 

|  token | email |
|---|---| 
|  string |  string |

5. The result is : 

```json
{
    "code": 4,
    "content": "Le Candidat a bien été supprimé",
    "success": true
}
```