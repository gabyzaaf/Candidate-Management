/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace Candidate_Management.CORE
{
    public class UserData
    {
        public string name {get;set;}
        public string firstname {get;set;}
        public string email{get;set;}
        public string mdp{get;set;}
        public bool read{get;set;}
        public bool update{get;set;}
        public bool delete{get;set;}

        public UserData(){

        }

        public UserData(string _name,string _firstname,string _email,string _mdp,bool _read,bool _update,bool _delete){
            name = _name;
            firstname = _firstname;
            email = _email;
            mdp = _mdp;
            read = _read;
            update = _update;
            delete = _delete;
        }
    }
}