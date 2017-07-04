using System;
using System.Collections;
using System.Collections.Generic;
using Core.Adapter.Inteface;
using core.configuration;
using scheduler;
using System.Globalization;
namespace Candidate_Management.CORE.Remind
{
    public class appellerRemind : LegacyRemind ,Iremind
    {
       
        public int id {get;set;}
        private string fileName = null; 
         
        public void add(int id,DateTime date){
            this.id = id;
            checkFileNameIsNull();
            IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
            this.date = date.AddDays(1);
            isql.remindType(id,date);
            remindId = isql.getLastCandidateIdFromRemind(id);
            emailCandidate = isql.getCandidateEmailFromId(id);
        }
        
        public void update(int id,DateTime date){
           this.id = id;
           checkFileNameIsNull();
           IsqlMethod isql = Factory.Factory.GetSQLInstance("mysql");
           this.date = date.AddDays(1);
           isql.updateRemindType(id,date);
           remindId = isql.getLastCandidateIdFromRemind(id);
           emailCandidate = isql.getCandidateEmailFromId(id);
        }

        
        

        public void exec(string token,DateTime meeting){
            checkFileNameIsNull();
            Dictionary<string,string> candidateInformation = getCandidateNameFromId(this.id);
            string pathAndFile = getPathNameAndFileFromTemplate(fileName);
           // string cmd = $"./script.sh {date.Hour}:{date.Minute} {date.Month}/{date.Day}/{date.Year}  {token} {pathAndFile} {candidateInformation["nom"]} {candidateInformation["prenom"]} {meeting}";
           string currentDate = $"{date.Month}/{date.Day}/{date.Year}";
           string currentHourMinute = $"{date.Hour}:{date.Minute}";
           string emailPluginPath = "/var/candidate/plugins/Candidate-Management/bin/Debug/netcoreapp2.0/email.dll";
           //string filePathTemplate = "/var/candidate/plugins/Candidate-Management/bin/Debug/netcoreapp2.0/sample.txt";
           string filePathTemplate = $"{JsonConfiguration.getInstance().getEmailTemplatePath()}{this.fileName}";
           string cmd = $"./script.sh {currentHourMinute} {currentDate}  {emailPluginPath} {remindId}  {filePathTemplate} {candidateInformation["nom"]} {candidateInformation["prenom"]} {currentDate} {emailCandidate}";
            traceOutSideTheSystem(cmd); 
            Schedule schedule = new Schedule(cmd);
            schedule.executeTask(); 
            
        }
    }
}