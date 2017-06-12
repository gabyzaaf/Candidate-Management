# This page is about how to add a report to the candidate : 

1. The url is ***POST***
***http://localhost:5000/api/User/add/candidat/report***

2. The content is in Json : 

```json
{
	"sessionId":"52",
	"emailCandidat":"gabriel.zaafrani@gmail.com",
	"note":"12",
	"link":"it's a link",
	"xpNote":"it's a xpNote",
	"nsNote":"ns note",
	"jobIdealNote":"jobIdealNote",
	"pisteNote":"piste note",
	"pieCouteNote":"pie coute note",
	"locationNote":"location note",
	"EnglishNote":"750",
	"nationalityNote":"French",
	"competences":"java programmer"
}
```

3. Information : 
The ***emailCandidat***  need to be already present inside the system.

4. The result : 

```json
{
    "code": 3,
    "content": "Le report a ete ajoute parfaitement à votre system",
    "success": true
}
``` 