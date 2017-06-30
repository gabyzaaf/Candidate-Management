# This page will explain how to get the datas from mlcandidate.

0. Information : 

You need to add the table ml candidate inside your database and add the datas present in the ***SQL FILE***.

You need to have the read access for get the datas.

1. The webservice type :

This webservice is ***GET***

2. The URL is :

***http://localhost:5000/api/Remind/stat/mlcandidate/{choice}/{token}***

3. The parameters : 

|  choice | token |
|---|---| 
|  string |  string |

choice : Is the choice like departement.
token : Is the user token.

3. Sample format result :

```json
[
    {
        "satisfaction_level": "0.38",
        "last_evaluation": "0.53",
        "number_project": "2",
        "average_montly_hours": "157",
        "time_spend_company": "3",
        "work_accident": "0",
        "promotion_last_5years": "0",
        "sales": "sales",
        "salary": "low"
    },
    {
        "satisfaction_level": "0.8",
        "last_evaluation": "0.86",
        "number_project": "5",
        "average_montly_hours": "262",
        "time_spend_company": "6",
        "work_accident": "0",
        "promotion_last_5years": "0",
        "sales": "sales",
        "salary": "medium"
    },
    {
        "satisfaction_level": "0.11",
        "last_evaluation": "0.88",
        "number_project": "7",
        "average_montly_hours": "272",
        "time_spend_company": "4",
        "work_accident": "0",
        "promotion_last_5years": "0",
        "sales": "sales",
        "salary": "medium"
    }
]
```

5. Sample bad format result if you indicate a bad choice : 

```json
{
    "code": 3,
    "content": "Votre choix departement n'existe pas ",
    "success": false
}
```

6. Choice list : 
accounting.
hr.
IT.
management.
marketing.
product_mng.
RandD.
sales.
support.
technical.