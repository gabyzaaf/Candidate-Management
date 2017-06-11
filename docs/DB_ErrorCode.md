# The error code about the database : 

1. The error code type : 
```json
{
    "code": 1,
    "content": "Authentication to host 'localhost' for user 'root' using method 'mysql_native_password' failed with message: Access denied for user 'root'@'localhost' (using password: YES)",
    "success": false
}
```
2. How to resolve this error code : 
You need to change inside your ***appsettings.json*** file the ***mysql password***