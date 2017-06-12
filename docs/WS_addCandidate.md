# This page is for explain how to add a candidate : 

1. The url : 

The url is type ***POST***, the url content is : 
***http://localhost:5000/api/User/add/candidat/***

2. the data is : 

```json
{
	"session_id":"98",
	"Name":"zaafrani",
	"Firstname":"Gabriel",
	"emailAdress":"gabriel.zaafrani@toto.com",
	"phone":"065434563",
	"sexe":"M",
	"action":"interne",
	"year":"2015",
	"link":"http://google.com",
	"crCall":"cr call",
	"ns":"NS",
	"email":"true"
}
```

3. The success report is : 

```json
{
    "code": 3,
    "content": "Le candidat a ete ajoute à votre systeme",
    "success": true
}
```

4. session ID : 
The session_id represent the ***Token***.