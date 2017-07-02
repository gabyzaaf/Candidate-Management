using System;
using System.IO;
namespace Candidate_Management.CORE
{
    public class UserFeature
    {
        public int jobId{get;set;}
        
        public string fileName{get;set;}

        public string candidateName{get;set;}

        public string candidateFirstname{get;set;}

        public DateTime dateMeeting{get;set;}

        public string candidateEmail {get;set;}

        public UserFeature(int _jobId,string _fileName,string _candidateName,string _candidateFirstname,DateTime _dateMeeting,string _candidateEmail){
            this.jobId = _jobId;
            this.fileName = _fileName;
            this.candidateName = _candidateName;
            this.candidateFirstname = _candidateFirstname;
            this.dateMeeting = _dateMeeting;
            this.candidateEmail = _candidateEmail;
            check();
        }

        public UserFeature(){

        }

        private void check(){
            if(this.jobId <= 0){
                throw new Exception("The id need to be supperrior to 0");
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