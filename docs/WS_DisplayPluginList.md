# This page will explain how to display the plugin list : 


1. Webservice type : 
The type of this webservice is ***GET***

2. The Webservice URL is :
***http://localhost:5000/api/Remind/display/plugins/list***

3. The results of this webservice is :

```json
[
    {
        "pluginName": "email"
    }
]
```

4. If you don't have plugin you will get this : 

```json
{
    "code": 3,
    "content": "Vous ne possedez aucun plugin",
    "success": false
}
```