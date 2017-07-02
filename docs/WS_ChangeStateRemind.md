# This page will explain how to change the email state.

0. Information : 
This Webservice will be call by at command (AT is a linux command).

1. Ws type :
The type of this webservice is ***POST***

2. The URL : 
***http://localhost:5000/api/Remind/change/job/state/***

3. The parameters to send: 

```json
{
	"jobId":"3",
    "fileName":"namefile",
    "candidateName":"zaaf",
    "candidateFirstname":"gaby",
    "dateMeeting":"05/09/2017",
    "candidateEmail":"candidat@gmail.com"
}
```

4. The result : 
```json
{
    "code": 1,
    "content": "Le job avec l'id 3 a été modifié ",
    "success": true
}
```

5. The exception result if the job number doesn't exist : 

```json
{
    "code": 3,
    "content": "Le job spécifié avec l'identifiant 300 n'existe pas ",
    "success": false
}
```

6. The exception result if the job number had already updated : 

```json
{
    "code": 3,
    "content": "Votre job avec l'identifiant 3 a déja été envoyé",
    "success": false
}
```
