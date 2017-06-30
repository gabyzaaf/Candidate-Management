using System;
namespace core.candidat{

    public class Candidat{

        public string session_id{get;set;}
        public string Name {get;set;}
        public string Firstname{get;set;}

        public string emailAdress{get;set;}
        public string phone{get;set;}

        public string zipcode{get;set;}
        public char sexe{get;set;}

        public string action{get;set;}

        public int year{get;set;}

        public string link{get;set;}

        public string crCall{get;set;}

        public string ns{get;set;}

        public bool email {get;set;}
        
        public string meetingNote{get;set;}

        public string linkMeeting {get;set;}

        public string xpNote{get;set;}

        public string nsNote{get;set;}

        public string jobIdeal{get;set;}

        public string pisteNote{get;set;}

        public string pieCouteNote{get;set;}

        public string locationNote{get;set;}

        public string englishNote{get;set;}

        public string nationalityNote{get;set;}

        public string competences{get;set;}


        public string crEntretien{get;set;}

        public int independant {get;set;}

        public Candidat(){

        }
 

        public Candidat(string _name,string _firstname,string _emailAdress,string _zipcode,string _phone,string _action,string _year,string _sexe,string _link,string _crCall,string _NS){
            this.Name = _name;
            this.Firstname = _firstname;
            this.emailAdress = _emailAdress;
            this.zipcode = _zipcode;
            this.phone = _phone;
            this.action = _action;
            this.sexe = _sexe[0];
            this.year = Int32.Parse(_year);
            this.link = _link;
            this.crCall = _crCall;
            this.ns = _NS;
        }

         public override string ToString()
        {
        return string.Format("[Name : {0},\n Firstname {1}]", Name, Firstname);
        }   
    }



}