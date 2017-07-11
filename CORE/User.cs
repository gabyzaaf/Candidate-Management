/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace core.user{

    public class User{
        
        public string sessionId{get;set;}
        public string name{get;set;}
        public string email {get;set;}
        public string password{get;set;}
       
        public User(){

        }

        public User(string _token,string _name, string _email,string _password){
            this.sessionId = _token;
            this.name = _name;
            this.email = _email;
            this.password = _password;
        }

       
    }

}

