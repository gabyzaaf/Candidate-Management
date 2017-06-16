# How to get email template titles.

1. The Web services type is ***GET***


2. The URL website is : 

 ***http://localhost:5000/api/candidate/email/template/titles/{token}/{lim1}/{lim2}***

|  token | lim1   | lim2   |
|---|---|---| 
|  string |  string | string|

3. For example : 

***http://localhost:5000/api/candidate/email/template/titles/25/0/200***

4. Result : 

```json
[
    {
        "titre_message": "f1"
    },
    {
        "titre_message": "f2"
    }
]
```