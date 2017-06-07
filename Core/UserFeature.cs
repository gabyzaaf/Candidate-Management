using System;
using System.IO;

namespace actions{

    public class UserFeatures{

        public int idJob{get;set;}
        public string fileName{get;set;}

        public string candidateName{get;set;}

        public string candidateFirstname{get;set;}

        public DateTime dateMeeting{get;set;}

        
        public UserFeatures(int _idJob,string _fileName,string _candidateName,string _candidateFirstname,DateTime _dateMeeting){
            this.idJob = _idJob;
            this.fileName = _fileName;
            this.candidateName = _candidateName;
            this.candidateFirstname = _candidateFirstname;
            this.dateMeeting = _dateMeeting;
            check();
        }

        private void check(){
            if(this.idJob <= 0){
                throw new Exception("the job id inside the database is <= 0");
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
        }

        public int getIdJob(){
            return this.idJob;
        }

        public string getFileName(){
            return this.fileName;
        }

        public string getCandidateName(){
            return this.candidateName;
        }

        public string getCandidateFirstname(){
            return this.candidateFirstname;
        }

        public DateTime getDateMeeting(){
            return this.dateMeeting;
        }

    }


}