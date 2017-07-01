using System;
using System.IO;

namespace actions{

    public class UserFeatures{

        public string userEmail{get;set;}
        public string fileName{get;set;}

        public string candidateName{get;set;}

        public string candidateFirstname{get;set;}

        public DateTime dateMeeting{get;set;}

        public string candidateEmail {get;set;}

        
        public UserFeatures(string _userEmail,string _fileName,string _candidateName,string _candidateFirstname,DateTime _dateMeeting,string _candidateEmail){
            this.userEmail = _userEmail;
            this.fileName = _fileName;
            this.candidateName = _candidateName;
            this.candidateFirstname = _candidateFirstname;
            this.dateMeeting = _dateMeeting;
            this.candidateEmail = _candidateEmail;
            check();
        }

        private void check(){
            if(String.IsNullOrEmpty(this.userEmail)){
                throw new Exception("the user email is empty");
            }
            if(String.IsNullOrEmpty(this.fileName)){
                throw new Exception("the file name is empty");
            }
            if(!File.Exists(this.fileName)){
                throw new Exception("the file from the path not exist");
            }
            if(String.IsNullOrEmpty(this.candidateName)){
                throw new Exception("the candidate name not exist");
            }
            if(String.IsNullOrEmpty(this.candidateFirstname)){
                throw new Exception("the candidate firstname not exist");
            }
            if(String.IsNullOrEmpty(this.candidateEmail)){
                throw new Exception("the candidate email not exist");
            }
        }

        
    }


}