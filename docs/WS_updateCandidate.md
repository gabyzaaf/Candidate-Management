# This page will explain how to update a candidate : 

1. This webservice is a type ***POST***

***This method is used for update the candidateAction***

2. The URL is  : 
    ***http://localhost:5000/api/User/update/candidat/***

3. The content is :

```json
{	
	"session_id":"81",
	"Name":"zaafrani",
	"Firstname":"David",
	"emailAdress":"david.zaafrani@toto.com",
	"phone":"065434563",
	"sexe":"M",
	"action":"appellerRemind",
	"zipcode":"75018",
	"year":"2015",
	"link":"http://google.com",
	"crCall":"cr call",
	"ns":"NS",
	"pluginType":"email"
}
```

4. The right Result is : 

```json
{
    "code": 4,
    "content": "Le candidat a ete modifie dans votre systeme",
    "success": true
}
```