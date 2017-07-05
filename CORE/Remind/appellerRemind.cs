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
       
        public int id {get;set;} // Is the candidat ID  
        private string choiceType =null;


        public void add(int id,DateTime date){
            this.id = id;
            checkFileNameIsNull();
            this.date = date.AddDays(1);
            isql.remindType(id,date);
            remindId = isql.getLastCandidateIdFromRemind(id);
            emailCandidate = isql.getCandidateEmailFromId(id);
        }
        
        public void update(int id,DateTime date){
           this.id = id;
           checkFileNameIsNull();
           this.date = date.AddDays(1);
           isql.updateRemindType(id,date);
           remindId = isql.getLastCandidateIdFromRemind(id);
           emailCandidate = isql.getCandidateEmailFromId(id);
        }

        
        

        public void exec(string token,DateTime meeting){
            checkFileNameIsNull();
            string filePathTemplate = $"{JsonConfiguration.getInstance().getEmailTemplatePath()}{this.fileName}";
            Dictionary<string,string> candidateInformation = getCandidateNameFromId(this.id);
            string pathAndFile = getPathNameAndFileFromTemplate(fileName);
            string currentDate = $"{date.Month}/{date.Day}/{date.Year}";
            string currentHourMinute = $"{date.Hour}:{date.Minute}";
            string emailPluginPath = getDllPathEmailFromEmailCandidat(emailCandidate);
            string cmd = $"./script.sh {currentHourMinute} {currentDate}  {emailPluginPath} {remindId}  {filePathTemplate} {candidateInformation["nom"]} {candidateInformation["prenom"]} {currentDate} {emailCandidate}";
            traceOutSideTheSystem(cmd); 
            Schedule schedule = new Schedule(cmd);
            schedule.executeTask(); 
            
        }
    }
}