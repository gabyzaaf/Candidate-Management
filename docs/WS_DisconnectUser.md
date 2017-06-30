# This page will explain how to disconnect an User : 

1. The Webservice Type : 

The webService type is ***GET***

2. The Url is :

***http://localhost:5000/api/User/Disconnect/{token}*** 

3. Parameters : 

|  token |
|---| 
|  string |

token : It's the user token.

4. Sample Result : 

Send a session Id Null to the client.

```json
{
    "sessionId": null,
    "name": null,
    "email": null,
    "password": null
}
```

