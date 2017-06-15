# This is the DotNet Core documentation

1. Configuration File.
2. Mandatory
3. EmailTemplate

## Configuration File.
For configure your application you need to configure the appsettings.json file : 

``` json
{
  "logPath":"/Users/zaafranigabriel/Documents/5A/Projet Annuel/final/Candidate-Management/logs/candidate.log",
  "pluginPath":"/Users/zaafranigabriel/Documents/5A/Projet Annuel/plugin/",
  "emailTemplate":"/Users/zaafranigabriel/Documents/5A/Projet Annuel/final/Candidate-Management/template/",
  "ErrorCode": {
      "FR":{
        "1":"Le token de l'utilisateur existe deja",
        "2":"L'utilisateur existe deja"
      },
      "EN":{
        "1":"The token already exist",
        "2":"User already exist"
      }  
  },
  "email":"juliec17@gmail.com",
  "SQL":{
    "connection":"server=localhost;user id=rootpassword=Password_A_Changer persistsecurityinfo=True;port=3306;database=candidate_management;SslMode=None"
  }
}

```

## Mandatory
You need to add the ***file path for the logPath***, if the file is not created he will be created.  

### EmailTemplate
You need to create the folder inside your system before to insert the link inside the ***appsettings.json*** in the ***emailTemplate***
